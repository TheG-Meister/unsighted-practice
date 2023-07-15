# UNSIGHTED Practice Mod
Unsighted Practice is a mod for 2021 indie metroidvania UNSIGHTED that provides cheats and tools to help speedrunners, glitch hunters and even casual players practice parts of the game. Features currently include:
* A bountiful settings menu with key rebinding and toggles for all features, courtesy of [ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager)
* Cheats that prevent cog depletion and fish-spot despawning
* 10 primitive save states that store all player data that is saved to the system, as well as some level data
* Hotkeys for resetting combo, reloading the current room and teleporting to the last terminal

## Installation

This setup guide is for v0.1.0.

__Note__ - if you are using the Xbox Gamepass version this tutorial may not work for you. Contact The G-Meister via the contact methods at the bottom of the page as there may be a way to get it to work.

### BepInEx

This mod runs using BepInEx, a mod manager for Unity games that allows multiple mods to exist side-by-side, and helps resolve conflicts between them. Installing BepInEx is simple and it can be uninstalled by simply renaming the `BepInEx` folder it creates. Once installed, installing a mod is as simple as putting the mod `.dll` file inside the `BepInEx/plugins/` folder.

If you already have BepInEx v5 installed, skip this section.
* Head to the [BepInEx v5.4.21 release page](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.21) and download `BepInEx_x64_5.4.21.0.zip`
* Extract the contents of this zip __in place__ to your game's root directory. If you've installed the game through Steam, this is usually `C:\Program Files (x86)\Steam\steamapps\common\Unsighted`. If unzipping creates a folder with the same name as the `.zip` file, move the contents of this folder (should just be a `BepInEx` folder) into the game's root directory
* Restart your computer
* Run the game once to generate configuration files. You'll know this has succeeded if the `BepInEx/plugins` folder exists in your game's directory

### Mods

From here on, all mods can be installed by downloading them from the links provided and extracting them __in place__ into your game's root directory.

#### Required

The following mods are required for Unsighted Practice to work smoothly:

* [Unsighted Practice](https://github.com/TheG-Meister/unsighted-practice/releases/tag/v0.1.0) `Unsighted Practice v0.1.0.zip` - this mod
* [ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager/releases/tag/v18.0.1) `BepInEx.ConfigurationManager_v18.0.1.zip` - used to change mod options during gameplay

#### Optional

The following mods aren't required, but may add some additional quality-of-life and even time-saving features to your modded gameplay:

* [UNSIGHTED++](https://github.com/Vheos/Mods.UNSIGHTED/releases/tag/v1.6.0) `UNSIGHTED++_v1.6.0_SteamGOG.zip` - allows the game's intros to be skipped. May conflict with some of the changes in here, it hasn't actually been tested
* [Graphics Settings](https://github.com/BepInEx/BepInEx.GraphicsSettings/releases/tag/v1.3) `BepInEx.GraphicsSettings_v1.3.zip` - a plugin for any Unity game that allows it to run in the background

## How to Use

* Once installed, hit `F1` on your keyboard. The Configuration Manager window should pop up displaying all the mods you have installed.
* Click on a mod to expand its options
* Hover over a setting to display a tooltip for what it does (if one is provided)
* In the Unsighted Practice menu, enable and disable any options you please
* Most hotkeys start unbound. To bind one, click the `Set` button, and press a button on your keyboard. This may also work with controller
* To use save states, hold the `Save` modifier and press the state key to save the current game state. This will also be written to a file in your game's install directory. To load it again, hold the `Load` modifier and press the state key. To see the effects of this load, you may have to transition screens, or use the `Reload current room` or `Teleport to last terminal` hotkey

## Contact

This mod is maintained by The G-Meister:
* Discord - @the_g_meister
* Email - [thegiemeister@gmail.com](mailto:thegiemeister@gmail.com)

I'm active in the Studio Pixel Punk Discord (invite can be found on UNSIGHTED's [speedrun.com page](https://www.speedrun.com/unsighted)). Issues and pull requests are always welcome \<3