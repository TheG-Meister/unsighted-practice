using dev.gmeister.unsighted.practice.core;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.cheats;

[Harmony]
public class InfiniteCogs
{

    [HarmonyPatch(typeof(BuffIconController), nameof(BuffIconController.ShowBuff)), HarmonyPrefix]
    public static void BeforeShowBuff(ref bool reduceSeconds)
    {
        if (Plugin.Instance.config.infiniteCogs.Value) reduceSeconds = false;
    }

    [HarmonyPatch(typeof(BuffsInterfaceController), nameof(BuffsInterfaceController.ReduceBuffCoroutine)), HarmonyPostfix]
    public static void BeforeReduceBuffCoroutine(ref IEnumerator __result)
    {
        if (Plugin.Instance.config.infiniteCogs.Value) __result = EmptyEnumerator();
    }

    private static IEnumerator EmptyEnumerator()
    {
        yield break;
    }

}
