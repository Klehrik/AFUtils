using System.Text;
using HarmonyLib;
using Il2Cpp;
using Il2CppQuantum;
using Il2CppQuantum_Core;
using Il2CppPhoton.Client;

namespace AFUtils;

/// <summary>
/// Allows for creating custom DeterministicCommands. <br></br>
/// Works by hijacking <c>SpecialActionCommand</c> and using unique action IDs.
/// </summary>
public class Command
{
    private static int idCounter = 20000;
    private static readonly Dictionary<int, string> idToIdentifier = new Dictionary<int, string>();
    private static readonly Dictionary<string, int> identifierToId = new Dictionary<string, int>();
    private static readonly Dictionary<string, Action<Frame>> callbacks = new Dictionary<string, Action<Frame>>();

    /// <summary>
    /// The unique identifier for this command.
    /// </summary>
    public string Identifier { get; }

    /// <param name="callback"><para>The function to call when the command is sent.</para> <para>It is called for every client, including the caller. <br></br>For the caller specifically, it is called multiple times (on every predicted frame <br></br>plus the final verified one); if you don't want this, add a check for <c>Frame.IsVerified</c></para></param>
    public Command(string identifier, Action<Frame> callback)
    {
        if (callbacks.ContainsKey(identifier))
        {
            throw new ArgumentException($"Identifier '{identifier}' is already in use.");
        }

        Identifier = identifier;
        callbacks[Identifier] = callback;

        var ID = idCounter++;
        idToIdentifier[ID] = identifier;
        identifierToId[identifier] = ID;
    }

    /// <summary>
    /// Executes the command for every client.
    /// </summary>
    /// <param name="error">The error message on a fail (usually when the local <c>Humanoid_View</c> does not exist yet).</param>
    /// <returns><c>true</c> if successful.</returns>
    public bool Send(out string error)
    {
        var game = QuantumRunner.Default?.Game;
        if (game == null)
        {
            error = $"Command '{Identifier}': 'QuantumRunner.Default.Game' is null";
            return false;
        }

        var view = Misc.GetLocalHumanoidView();
        if (view == null)
        {
            error = $"Command '{Identifier}': Cannot find local Humanoid_View";
            return false;
        }

        var cmd = new SpecialActionCommand
        {
            action = identifierToId[Identifier],
            player = view.playerEntityRef
        };
        game.SendCommand(cmd);

        error = "";
        return true;
    }

    /// <summary>
    /// Executes the command for every client.
    /// </summary>
    /// <returns><c>true</c> if successful.</returns>
    public bool Send()
    {
        return Send(out _);
    }

    [HarmonyPatch(typeof(SpecialActionCommand), nameof(SpecialActionCommand.Execute))]
    public static class SpecialActionCommandPatch
    {
        static bool Prefix(SpecialActionCommand __instance, Frame f)
        {
            // Intercept action IDs that are used here
            var ID = __instance.action;
            if (idToIdentifier.ContainsKey(ID))
            {
                var identifier = idToIdentifier[ID];
                if (callbacks.ContainsKey(identifier))
                {
                    // This will run multiple times for the caller
                    // (on every predicted frame and the final verified one)
                    // Check for `f.IsVerified` to have it only run once
                    callbacks[identifier](f);
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(PlayerJoinSystem), nameof(PlayerJoinSystem.OnPlayerAdded))]
    public static class PlayerJoinSystemPatch
    {
        static void Postfix(PlayerJoinSystem __instance)
        {
            var controllerInstance = PhotonController.instance;
            if (controllerInstance.IsMasterClient())
            {
                var client = controllerInstance.client;
                var room = client.CurrentRoom;
                if (client.CurrentRoom != null)
                {
                    // Send the host's command identifier<->ID mapping to all players
                    var commandMapping = new PhotonHashtable();
                    foreach (var command in identifierToId)
                    {
                        commandMapping.Add(command.Key, command.Value.ToString());
                        // ^ Need to do int->string or the game will hang when exiting a room
                    }
                    var properties = new PhotonHashtable();
                    properties.Add("AFUtils_Command", commandMapping);
                    client.LocalPlayer.SetCustomProperties(properties, null);
                }
            }
        }
    }

    [HarmonyPatch(typeof(PhotonController), nameof(PhotonController.OnPlayerPropertiesUpdate))]
    public static class PhotonControllerPatch
    {
        static void Postfix(Il2CppPhoton.Realtime.Player targetPlayer, PhotonHashtable changedProps)
        {
            if (targetPlayer.IsLocal) return;
            if (!changedProps.ContainsKey("AFUtils_Command")) return;

            // Convert PhotonHashtable to Dictionary (ContainsKey is used later)
            var commands = changedProps["AFUtils_Command"].Cast<PhotonHashtable>();
            var incoming = new Dictionary<string, int>();
            foreach (var command in commands)
            {
                var identifier = command.Key.ToString();
                var ID = int.Parse(command.Value.ToString());
                incoming[identifier] = ID;
            }

            var sb = new StringBuilder("Incoming host command mappings:");
            foreach (var command in incoming)
            {
                var identifier = command.Key;
                var ID = command.Value;
                sb.Append("\n" + ID + ": " + identifier);

                // Increment `idCounter` if it is lower than `ID`
                // to not return one possibly already in use
                idCounter = Math.Max(idCounter, ID + 1);

                // If there is a current mapping at `ID`,
                // move it to a new one
                if (idToIdentifier.ContainsKey(ID))
                {
                    var existingIdentifier = idToIdentifier[ID];
                    if (existingIdentifier != identifier)
                    {
                        var newID = idCounter++;

                        // Do not set if it is in the incoming mapping,
                        // since it will be handled (or has been already)
                        if (!incoming.ContainsKey(existingIdentifier))
                        {
                            idToIdentifier[newID] = existingIdentifier;
                            identifierToId[existingIdentifier] = newID;
                        }
                    }
                }

                // Set new mapping
                idToIdentifier[ID] = identifier;
                identifierToId[identifier] = ID;
            }
            Core.Logger.Msg(sb);
        }
    }
}