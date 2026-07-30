using MelonLoader;

[assembly: MelonInfo(typeof(AFUtils.Core), "AFUtils", "1.0.0", "Klehrik", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace AFUtils;

public class Core : MelonMod
{
    internal static MelonLogger.Instance Logger => Melon<Core>.Logger;
}