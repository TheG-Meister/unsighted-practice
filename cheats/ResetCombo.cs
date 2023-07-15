using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dev.gmeister.unsighted.practice.core;
using HarmonyLib;

namespace dev.gmeister.unsighted.practice.cheats;

[Harmony]
public class ResetCombo
{

    private ComboBar comboBar;

    public ResetCombo(ComboBar comboBar)
    {
        this.comboBar = comboBar;
    }

    public void RemoveAllCombo()
    {
        this.comboBar.AddComboValue(-10000f);
    }

    [HarmonyPatch(typeof(ComboBar), nameof(ComboBar.Start)), HarmonyPostfix]
    public static void AfterComboBarStart(ComboBar __instance)
    {
        Plugin.Instance.resetCombo = new ResetCombo(__instance);
    }

}
