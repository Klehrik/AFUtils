using MelonLoader;

[assembly: MelonInfo(typeof(AFUtils.Core), "AFUtils", "1.0.0", "Klehrik", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace AFUtils;

public class Core : MelonMod
{
    // TESTING - Remove before publishing
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
                ActionMenu.AddOption(option, "some text");
            }
        );
    }
}