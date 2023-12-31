﻿using BepInEx.Configuration;
using dev.gmeister.unsighted.practice.cheats;
using dev.gmeister.unsighted.practice.unsighted;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace dev.gmeister.unsighted.practice.core;

public class Config
{

    private ConfigFile config;
    private SaveStates states;

    public const string CATEGORY_CHEATS = "Cheats";
    public ConfigEntry<bool> infiniteCogs;
    public ConfigEntry<bool> infiniteFishing;
    public ConfigEntry<bool> debugMode;

    public const string CATEGORY_COMBO = "Combo";
    public ConfigEntry<KeyCode> resetComboKey;

    public const string CATEGORY_QUICKSAVE_LOAD = "Quick Save/Load";
    public ConfigEntry<KeyCode> saveModifier;
    public ConfigEntry<KeyCode> loadModifier;
    public List<ConfigEntry<KeyCode>> slotKeys;

    public const string CATEGORY_SAVE_STATES = "Save States";
    public ConfigEntry<string> saveStateName;
    public ConfigEntry<bool> saveNewState;
    public ConfigEntry<string> selectedState;
    public ConfigEntry<bool> saveState;
    public ConfigEntry<bool> loadState;
    public ConfigEntry<bool> deleteState;
    public ConfigEntry<KeyCode> loadStateKey;

    public const string CATEGORY_LOCATION = "Player Location";
    public ConfigEntry<KeyCode> teleportToTerminal;
    public ConfigEntry<KeyCode> reloadRoom;
    public ConfigEntry<bool> quickReload;

    public Config(ConfigFile config, SaveStates states)
    {
        this.config = config;
        this.states = states;

        this.infiniteCogs = config.Bind(CATEGORY_CHEATS, "Infinite cogs", false, "Prevents cogs from decreasing in use or timing out");
        this.infiniteFishing = config.Bind(CATEGORY_CHEATS, "Infinite fishing", false, "Prevents fishing spots from being consumed");
        this.debugMode = config.Bind(CATEGORY_CHEATS, "Debug mode", false, "Enables debug mode, which allows starting the game from different areas as well as an in-game cheats menu, accessible from the options pace");

        this.resetComboKey = config.Bind(CATEGORY_COMBO, "Reset combo key", KeyCode.None, "Use this input to reset the combo bar to a 1x modifier");

        this.saveModifier = config.Bind(CATEGORY_QUICKSAVE_LOAD, "Save state modifier", KeyCode.LeftControl, "Hold this modifier and press a save state button to save the current game to this state");
        this.loadModifier = config.Bind(CATEGORY_QUICKSAVE_LOAD, "Load state modifier", KeyCode.LeftShift, "Hold this modifier and press a save state button to load the state into the current game");
        this.slotKeys = new();
        for (int i = 0; i < 10; i++)
        {
            slotKeys.Add(config.Bind(CATEGORY_QUICKSAVE_LOAD, $"Slot {i + 1} key", KeyCode.None, $"The button to press to interact with save state {i}"));
        }

        this.saveStateName = config.Bind(CATEGORY_SAVE_STATES, "New state name", "state", new ConfigDescription("The name of a new save state. If a state with this name already exists, it will be overwritten when saved to.", null, new ConfigurationManagerAttributes { Order = -6 }));
        this.saveNewState = config.Bind(CATEGORY_SAVE_STATES, "Create new state", false, new ConfigDescription("Save the current game state to a file. You may need to close and reopen the Configuration Manager window for new states to show up in the load menu.", null, new ConfigurationManagerAttributes { Order = -7, CustomDrawer = Config.CreateButtonDrawer("Create", (entry) => {
            this.states.CreateAndWrite(this.saveStateName.Value);
            this.UpdateStatesList();
        }) }));
        this.UpdateStatesList();

        this.loadState = config.Bind(CATEGORY_SAVE_STATES, "Load", false, new ConfigDescription("Load the selected save state.", null, new ConfigurationManagerAttributes { Order = -2, CustomDrawer = Config.CreateButtonDrawer("Load", (entry) => {
            if (this.states.Exists(this.selectedState.Value)) this.states.ReadAndLoad(this.selectedState.Value);
        }) }));

        this.saveState = config.Bind(CATEGORY_SAVE_STATES, "Save", false, new ConfigDescription("Save the current game state to the selected save state.", null, new ConfigurationManagerAttributes { Order = -3, CustomDrawer = Config.CreateButtonDrawer("Save", (entry) => {
            this.states.CreateAndWrite(this.selectedState.Value);
            this.UpdateStatesList();
        }) }));

        this.deleteState = config.Bind(CATEGORY_SAVE_STATES, "Delete", false, new ConfigDescription("Delete the selected state. You may have to reopen the Configuration Manager window in order for the state to disappear from the list.", null, new ConfigurationManagerAttributes { Order = -4, CustomDrawer = Config.CreateButtonDrawer("Delete", (entry) => {
            if (this.states.Exists(this.selectedState.Value))
            {
                this.states.Delete(this.selectedState.Value);
                this.UpdateStatesList();
            }
        }) }));

        this.loadStateKey = config.Bind(CATEGORY_SAVE_STATES, "Load state key", KeyCode.None, new ConfigDescription("The key to press to load the selected state", null, new ConfigurationManagerAttributes { Order = -5 }));

        this.teleportToTerminal = config.Bind(CATEGORY_LOCATION, "Teleport to last terminal", KeyCode.None, "Use this input to teleport the player to their last terminal or checkpoint");
        this.reloadRoom = config.Bind(CATEGORY_LOCATION, "Reload current room", KeyCode.None, "Use this input to reload the current room. Works well with the current save states");
        this.quickReload = config.Bind(CATEGORY_LOCATION, "Faster scene reload", false, "Enables a faster reload that drops inputs and looks visually glitchy");
    }

    public static Action<ConfigEntryBase> CreateButtonDrawer(string text, Action<ConfigEntryBase> onPress)
    {
        return (ConfigEntryBase entry) =>
        {
            if (GUILayout.Button(text, GUILayout.ExpandWidth(true)))
            {
                onPress(entry);
            }
        };
    }

    public void UpdateStatesList()
    {
        if (this.selectedState != null) this.config.Remove(this.selectedState.Definition);

        List<string> states = this.states.GetAllStates();
        if (states.Count < 1) states.Add(string.Empty);
        this.selectedState = this.config.Bind(CATEGORY_SAVE_STATES, "Current state", string.Empty, new ConfigDescription("Select a state", new AcceptableValueList<string>(states.ToArray()), new ConfigurationManagerAttributes { Order = -1 }));
    }

}
