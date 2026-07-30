using MelonLoader;

[assembly: MelonInfo(typeof(AFUtils.Core), "AFUtils", "1.0.0", "Klehrik", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace AFUtils;

public class Core : MelonMod
{
    // TESTING - Remove before publishing
    public Command cmd;

    public override void OnInitializeMelon()
    {
        var option = new ActionMenu.Option(
            () =>
            {
                LoggerInstance.Msg("Jaboner!");
            },
            "jaboner"
        );
        
        ActionMenu.RegisterForCollection(() =>
            {
                ActionMenu.AddOption(option);
                ActionMenu.AddOption(option, "specific text");
            }
        );

        cmd = new Command(
            (Il2CppQuantum.Frame f) =>
            {
                LoggerInstance.Msg("Ja... rona?");
                LoggerInstance.Msg("Frame is " + f.Number);
            }
        );
    }

    public override void OnUpdate()
    {
        if (UnityEngine.InputSystem.Keyboard.current.hKey.wasPressedThisFrame)
        {
            LoggerInstance.Msg("Sending command!");
            cmd.Send();
        }
    }
}