using UnityEngine;
using Il2Cpp;
using Il2CppView_Humanoid;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppPhoton.Realtime;
using Il2CppPhoton.Client;

namespace AFUtils;

public static class Misc
{
    private static Humanoid_View localHumanoidView;

    public static Humanoid_View GetLocalHumanoidView()
    {
        if (localHumanoidView != null) return localHumanoidView;

        Il2CppArrayBase<Humanoid_View> views = GameObject.FindObjectsOfType<Humanoid_View>();
        foreach (var view in views)
        {
            if (view.isLocal)
            {
                localHumanoidView = view;
                return view;
            }
        }
        return null;
    }
}