using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dev.gmeister.unsighted.practice.core;
using HarmonyLib;

namespace dev.gmeister.unsighted.practice.cheats;

[Harmony]
public class ComboCheat
{

    public ComboBar comboBar;

    public ComboCheat(ComboBar comboBar)
    {
        this.comboBar = comboBar;
    }

    public void RemoveAllCombo()
    {
        this.SetCombo(1f);
    }

    public void SetCombo(float value)
    {
        this.comboBar.comboValue = value;
        this.comboBar.UpdateComboBar();
    }

    [HarmonyPatch(typeof(ComboBar), nameof(ComboBar.Start)), HarmonyPostfix]
    public static void AfterComboBarStart(ComboBar __instance)
    {
        Plugin.Instance.comboCheat = new ComboCheat(__instance);
    }

}
