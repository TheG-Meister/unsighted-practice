using BepInEx;
using BepInEx.Configuration;
using dev.gmeister.unsighted.practice.cheats;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
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
    public QuickSaves quickSaves;
    public SaveStates states;

    public static Plugin Instance { get; private set; }

    public Plugin()
    {
        if (Plugin.Instance != null) throw new InvalidOperationException("An instance of Plugin already exists");
        Plugin.Instance = this;

        this.CheckForVersionChange();

        this.states = new(SAVE_STATES_PATH);

        this.config = new(this.Config, this.states);

        this.quickSaves = new(10);

        if (this.config.debugMode.Value) cheats.Debug.SetDebug(true);
        this.config.debugMode.SettingChanged += (o, v) => cheats.Debug.SetDebug(this.config.debugMode.Value);

        this.harmony = new Harmony(PLUGIN_GUID);
        this.harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public void Update()
    {
        if (this.resetCombo != null)
        {
            if (Input.GetKeyDown(this.config.resetComboKey.Value)) this.resetCombo.RemoveAllCombo();
        }

        bool save = this.config.saveModifier.Value == KeyCode.None || Input.GetKey(this.config.saveModifier.Value);
        bool load = this.config.loadModifier.Value == KeyCode.None || Input.GetKey(this.config.loadModifier.Value);

        for (int i = 0; i < this.config.slotKeys.Count; i++)
        {
            ConfigEntry<KeyCode> stateButton = this.config.slotKeys[i];
            if (save && Input.GetKeyDown(stateButton.Value)) this.quickSaves.QuickSave(i);
            if (load && Input.GetKeyDown(stateButton.Value)) this.quickSaves.QuickLoad(i);
        }

        if (Input.GetKeyDown(this.config.teleportToTerminal.Value)) ReenterScene.Respawn(!this.config.quickReload.Value);
        if (Input.GetKeyDown(this.config.reloadRoom.Value)) ReenterScene.Reenter(!this.config.quickReload.Value);

        if (Input.GetKeyDown(this.config.loadStateKey.Value))
        {
            if (this.states.Exists(this.config.selectedState.Value)) this.states.ReadAndLoad(this.config.selectedState.Value);
        }

    }

    public void CheckForVersionChange()
    {
        if (Directory.Exists(SAVE_STATES_PATH) && !Directory.Exists(QUICKSAVES_PATH))
        {
            Directory.Move(SAVE_STATES_PATH, QUICKSAVES_PATH);
            Directory.CreateDirectory(SAVE_STATES_PATH);
        }
    }
}
