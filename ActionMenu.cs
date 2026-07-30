using HarmonyLib;
using Il2CppHUD;
using Il2CppCustomUIRenderingAccess;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace AFUtils;

public class ActionMenu
{
    private static readonly List<Action> callbacks = new List<Action>();
    private static readonly List<(int, string)> collected = new List<(int, string)>();

    public static void RegisterForCollection(Action callback)
    {
        callbacks.Add(callback);
    }

    public static void AddOption(Option option, string label)
    {
        collected.Add((option.ID, label));
    }

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

    public class Option
    {
        private static int idCounter = 10000;
        internal static readonly Dictionary<int, Action> callbacks = new Dictionary<int, Action>();

        public int ID { get; }
        public string Label { get; set; }

        public Option(Action callback, string label)
        {
            ID = idCounter;
            idCounter++;
            Label = label;
            callbacks[ID] = callback;
        }

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