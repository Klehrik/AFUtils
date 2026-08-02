using HarmonyLib;
using Il2CppHUD;
using Il2CppCustomUIRenderingAccess;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace AFUtils;

/// <summary>
/// Allows for adding new options to the action menu. <br></br>
/// Works by hijacking <c>SpecialActionCommand</c> and using unique action IDs.
/// </summary>
public static class ActionMenu
{
    private static readonly List<Action> callbacks = new List<Action>();
    private static readonly List<(int, string)> collected = new List<(int, string)>();
    private static bool collecting = false;

    /// <summary>
    /// Registers a function to be called whenever option collection happens for the menu.
    /// </summary>
    /// <param name="callback"><c>AddOption</c> should be called in here to add options.</param>
    public static void RegisterForCollection(Action callback)
    {
        callbacks.Add(callback);
    }

    /// <summary>
    /// Adds an option to the action menu.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddOption(Option option, string label)
    {
        if (!collecting)
        {
            throw new InvalidOperationException("Cannot add options outside of collection time.");
        }
        collected.Add((option.ID, label));
    }

    /// <summary>
    /// Adds an option to the action menu.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddOption(Option option)
    {
        AddOption(option, option.Label);
    }

    private static void CollectOptions()
    {
        foreach (var callback in callbacks)
        {
            callback();
        }
    }

    /// <summary>
    /// Represents an action menu option.
    /// </summary>
    public class Option
    {
        private static int idCounter = 10000;
        internal static readonly Dictionary<int, Action> callbacks = new Dictionary<int, Action>();

        /// <summary>
        /// The unique action ID of the option. <br></br>
        /// These start at <c>10000</c>.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// The default label for the option. <br></br>
        /// Used if one is not explicitly passed to <c>AddOption</c>.
        /// </summary>
        public string Label { get; set; }

        /// <param name="callback">The function to call when the option is selected.</param>
        public Option(Action callback, string label)
        {
            ID = idCounter++;
            Label = label;
            callbacks[ID] = callback;
        }

        /// <param name="callback">The function to call when the option is selected.</param>
        public Option(Action callback) : this(callback, "unknown")
        {
        }
    }

    [HarmonyPatch(typeof(RadialMenu_Component))]
    public static class RadialMenu_ComponentPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(RadialMenu_Component.RefreshItems))]
        static void RefreshItemsPrefix(RadialMenu_Component __instance)
        {
            if (__instance.type == HUD_Component.HUDComponentType.Radial_SpecialAction)
            {
                collecting = true;
                CollectOptions();
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(RadialMenu_Component.RefreshItems))]
        static void RefreshItemsPostfix(RadialMenu_Component __instance)
        {
            if (collected.Count <= 0) return;

            var items = __instance.items;
            foreach (var option in collected)
            {
                items.Add(option.Item1);
            }

            // Need to clear so it doesn't bleed into the other
            // menus since they are all `RadialMenu_Component`s
            collected.Clear();
            collecting = false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(RadialMenu_Component.HandleSelection))]
        static void Postfix(Il2CppHUD.RadialMenu_Component __instance, ref int selectedItemIndex, ref int selectedItemValue)
        {
            // Intercept action IDs that are used here
            if (Option.callbacks.ContainsKey(selectedItemValue))
            {
                Option.callbacks[selectedItemValue]();

                // Set these to `-1` so they don't end
                // up doing anything for other players.
                // Intercepting this method does not prevent DeterministicCommands
                // from being queued, since that happens in the body.
                // I tried to fix this by intercepting Serialize writing but despite
                // my attempts I have no idea what the format is for the BitStream.
                selectedItemIndex = -1;
                selectedItemValue = -1;
            }
        }
    }

    [HarmonyPatch(typeof(RadialMenuGraphic), nameof(RadialMenuGraphic.SetItems))]
    public static class RadialMenuGraphicPatch
    {
        static void Prefix(RadialMenuGraphic __instance, ref Il2CppStringArray items)
        {
            if (collected.Count <= 0) return;

            var ogCount = items.Length;
            string[] labels = new string[ogCount + collected.Count];

            // Add all original options to new array
            for (var i = 0; i < ogCount; i++)
            {
                labels[i] = items[i];
            }

            // Add collected options
            var j = ogCount;
            foreach (var option in collected)
            {
                labels[j] = option.Item2;
                j++;
            }

            items = new Il2CppStringArray(labels);
        }
    }
}