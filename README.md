# AFUtils
Small miscellaneous library for modding Airframe Ultra Beta v0.34.

## Setup
- Download `AFUtils.dll` (and optionally `AFUtils.xml` for documentation) and place them in the `Mods` folder.
- In Visual Studio, add it as a Shared Project Reference.  
  <img width="286" height="104" alt="image" src="https://github.com/user-attachments/assets/5d545698-4ba6-4662-b0f4-fb5f2fc025c5" />  
  <img width="444" height="107" alt="image" src="https://github.com/user-attachments/assets/caa56cdc-e211-4aae-9ecd-804103253633" />  
- Add this assembly attribute: `[assembly: MelonAdditionalDependencies("AFUtils")]`

## Features
### ActionMenu
```cs
using AFUtils;

// ...

var option = new ActionMenu.Option(
    // This function will run when the option is selected
    () =>
    {
        LoggerInstance.Msg("Hello world!");
    },

    // Set default option label
    "hello world"
);

// This function will run when options are being collected for the menu
ActionMenu.RegisterForCollection(
    () =>
    {
        // Add option with default label
        ActionMenu.AddOption(option);

        // Add option with specified text
        ActionMenu.AddOption(option, "specific text");
    }
);
```

### Command
```cs
using AFUtils;

// ...

var cmd = new Command(
    // The unique identifier for the command; consider prepending your mod name or something
    "myMod_myCommand",

    // This function will run when the command is sent
    // It executes for all players, including the caller
    (Il2CppQuantum.Frame f) =>
    {
        LoggerInstance.Msg("AAAAHHHRGH!");

        // For the caller specifically, this will run
        // multiple times (on every predicted frame + the final verified one)
        // If you don't want this, add a check for `f.IsVerified`
        if (!f.IsVerified) return;
        LoggerInstance.Msg("This line prints once");
    }
);

// Execute the command for all players
cmd.Send();
```

### Packet
```cs
using AFUtils;

var packet = new Packet(
    // The unique identifier for the packet; consider prepending your mod name or something
    "myMod_myPacket",

    // This function will run when the packet is received
    // It executes for all clients excluding the sender
    (Il2CppPhoton.Realtime.Player player, Dictionary<string, string> data) =>
    {
        LoggerInstance.Msg($"{data["abc"]}, {data["def"]}");
    }
);

// ...

// Send the packet to all other clients
// Returns `true` if successful
packet.Send(
    new Dictionary<string, string>
    {
        ["abc"] = "123",
        ["def"] = "456",
    }
);
```

### Misc
```cs
// Returns the Humanoid_View belonging to the local player.
public static Humanoid_View GetLocalHumanoidView()

// Returns `true` if this client is the room host.
// Always `true` outside of an online room.
public static bool IsHost()

// Prints a list of components belonging to a Quantum entity.
public static void PrintComponentList(EntityRef r)
```
