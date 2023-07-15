using dev.gmeister.unsighted.practice.core;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.cheats;

[Harmony]
public class InfiniteFishing
{

    [HarmonyPatch(typeof(FishSpot), nameof(FishSpot.CheckIfDisappear)), HarmonyPrefix]
    public static bool StopFishSpotDisappearing()
    {
        return !Plugin.Instance.config.infiniteFishing.Value;
    }

}
