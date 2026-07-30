using UnityEngine;
using Il2CppView_Humanoid;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace AFUtils;

public class Player
{
    private static Humanoid_View localPlayerView;

    public static Humanoid_View GetLocal()
    {
        if (localPlayerView != null) return localPlayerView;

        Il2CppArrayBase<Humanoid_View> views = GameObject.FindObjectsOfType<Humanoid_View>();
        foreach (var view in views)
        {
            if (view.isLocal)
            {
                localPlayerView = view;
                return view;
            }
        }
        return null;
    }
}