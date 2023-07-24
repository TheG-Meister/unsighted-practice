using BepInEx;
using BepInEx.Configuration;
using dev.gmeister.unsighted.practice.cheats;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static dev.gmeister.unsighted.practice.core.Constants;

namespace dev.gmeister.unsighted.practice.core;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{

    public Config config;
    public Harmony harmony;

    public ResetCombo resetCombo;
    public QuickIO quickIO;

    public static Plugin Instance { get; private set; }

    public Plugin()
    {
        if (Plugin.Instance != null) throw new InvalidOperationException("An instance of Plugin already exists");
        Plugin.Instance = this;

        this.config = new Config(this.Config);

        this.quickIO = new QuickIO(10);

        cheats.Debug.SetDebug(true);

        this.harmony = new Harmony(PLUGIN_GUID);
        this.harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public void Update()
    {
        if (this.resetCombo != null)
        {
            if (Input.GetKeyDown(this.config.resetComboButton.Value)) this.resetCombo.RemoveAllCombo();
        }

        bool save = this.config.saveModifier.Value == KeyCode.None || Input.GetKey(this.config.saveModifier.Value);
        bool load = this.config.loadModifier.Value == KeyCode.None || Input.GetKey(this.config.loadModifier.Value);

        for (int i = 0; i < this.config.slotsButtons.Count; i++)
        {
            ConfigEntry<KeyCode> stateButton = this.config.slotsButtons[i];
            if (save && Input.GetKeyDown(stateButton.Value)) this.quickIO.QuickSave(i);
            if (load && Input.GetKeyDown(stateButton.Value)) this.quickIO.QuickLoad(i);
        }

        if (Input.GetKeyDown(this.config.teleportToTerminal.Value)) ReenterScene.Respawn(!this.config.quickReload.Value);
        if (Input.GetKeyDown(this.config.reloadRoom.Value)) ReenterScene.Reenter(!this.config.quickReload.Value);
    }

}
