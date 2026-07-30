using HarmonyLib;
using Il2CppQuantum;

namespace AFUtils;

public class Command
{
    private static int idCounter = 20000;
    private static readonly Dictionary<int, Action<Frame>> callbacks = new Dictionary<int, Action<Frame>>();

    public int ID { get; }

    public Command(Action<Frame> callback)
    {
        ID = idCounter++;
        callbacks[ID] = callback;
    }

    public bool Send()
    {
        var game = QuantumRunner.Default?.Game;
        if (game == null) return false;

        var cmd = new SpecialActionCommand();
        cmd.action = ID;
        cmd.player = Player.GetLocal().playerEntityRef;
        game.SendCommand(cmd);

        return true;
    }

    [HarmonyPatch(typeof(SpecialActionCommand), nameof(SpecialActionCommand.Execute))]
    public static class SpecialActionCommandPatch
    {
        static bool Prefix(SpecialActionCommand __instance, Frame f)
        {
            // Intercept action IDs that are used here
            var ID = __instance.action;
            if (callbacks.ContainsKey(ID))
            {
                // Prevent execution for the caller
                // This is because Execute runs multiple times
                // for the caller for whatever reason
                if (__instance.player != Player.GetLocal().playerEntityRef)
                {
                    callbacks[ID](f);
                }
                return false;
            }
            return true;
        }
    }
}