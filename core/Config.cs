using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dev.gmeister.unsighted.practice.core;

public class Config
{

    public const string CATEGORY_CHEATS = "Cheats";
    public ConfigEntry<bool> infiniteCogs;
    public ConfigEntry<bool> infiniteFishing;
    public ConfigEntry<bool> debugMode;

    public const string CATEGORY_COMBO = "Combo";
    public ConfigEntry<KeyCode> resetComboButton;

    public const string CATEGORY_STATES = "Save States";
    public ConfigEntry<KeyCode> saveModifier;
    public ConfigEntry<KeyCode> loadModifier;
    public List<ConfigEntry<KeyCode>> slotsButtons;

    public const string CATEGORY_LOCATION = "Player Location";
    public ConfigEntry<KeyCode> teleportToTerminal;
    public ConfigEntry<KeyCode> reloadRoom;
    public ConfigEntry<bool> quickReload;

    public Config(ConfigFile config)
    {
        this.infiniteCogs = config.Bind(CATEGORY_CHEATS, "Infinite cogs", false, "Prevents cogs from decreasing in use or timing out");
        this.infiniteFishing = config.Bind(CATEGORY_CHEATS, "Infinite fishing", false, "Prevents fishing spots from being consumed");
        this.debugMode = config.Bind(CATEGORY_CHEATS, "Debug mode", false, "Enables debug mode, which allows starting the game from different areas as well as an in-game cheats menu, accessible from the options pace");

        this.resetComboButton = config.Bind(CATEGORY_COMBO, "Reset combo button", KeyCode.None, "Use this input to reset the combo bar to a 1x modifier");

        this.saveModifier = config.Bind(CATEGORY_STATES, "Save state modifier", KeyCode.LeftControl, "Hold this modifier and press a save state button to save the current game to this state");
        this.loadModifier = config.Bind(CATEGORY_STATES, "Load state modifier", KeyCode.LeftShift, "Hold this modifier and press a save state button to load the state into the current game");
        this.slotsButtons = new();
        for (int i = 0; i < 10; i++)
        {
            slotsButtons.Add(config.Bind(CATEGORY_STATES, $"Slot {i + 1} button", KeyCode.None, $"The button to press to interact with save state {i}"));
        }

        this.teleportToTerminal = config.Bind(CATEGORY_LOCATION, "Teleport to last terminal", KeyCode.None, "Use this input to teleport the player to their last terminal or checkpoint");
        this.reloadRoom = config.Bind(CATEGORY_LOCATION, "Reload current room", KeyCode.None, "Use this input to reload the current room. Works well with the current save states");
        this.quickReload = config.Bind(CATEGORY_LOCATION, "Faster scene reload", false, "Enables a faster reload that drops inputs and looks visually glitchy");
    }
}
