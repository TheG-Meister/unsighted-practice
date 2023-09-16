using dev.gmeister.unsighted.practice.core;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace dev.gmeister.unsighted.practice.cheats;

[Harmony]
public class ReenterScene
{

    public static Type lastTransitionType = null;
    public static string lastLadder = null;
    public static string lastCraterTowerElevator = null;

    public static void Respawn(bool transition)
    {
        if (!PlayerInfo.cutscene && PlayerInfo.AtLeastOnePlayerAlive() && !gameTime.paused)
        {
            LevelController.restartingPlayer = true;
            if (PseudoSingleton<GlobalGameData>.instance.currentData.playerDataSlots[PseudoSingleton<GlobalGameData>.instance.loadedSlot].lastTerminalData == null || string.IsNullOrEmpty(PseudoSingleton<Helpers>.instance.GetPlayerData().lastTerminalData.sceneName))
            {
                PseudoSingleton<MapManager>.instance.LoadRoom(PseudoSingleton<Helpers>.instance.GetPlayerData().lastCheckpoint.sceneName, transition);
            }
            else
            {
                PseudoSingleton<MapManager>.instance.LoadRoom(PseudoSingleton<Helpers>.instance.GetPlayerData().lastTerminalData.sceneName, transition);
            }
        }
    }

    public static void Reenter(bool transition)
    {
        if (!PlayerInfo.cutscene && PlayerInfo.AtLeastOnePlayerAlive() && !gameTime.paused)
        {
            if (ScreenTransition.lastSceneName == null) ReenterScene.Respawn(transition);
            else
            {
                PlayerInfo.cutscene = true;

                if (ReenterScene.lastTransitionType == typeof(SceneChangeLadder)) SceneChangeLadder.currentLadder = ReenterScene.lastLadder;
                else if (ReenterScene.lastTransitionType == typeof(CraterTowerElevator)) CraterTowerElevator.currentElevator = ReenterScene.lastCraterTowerElevator;
                else if (ReenterScene.lastTransitionType == typeof(ScreenTransition)) ScreenTransition.playerTransitioningScreens = true;
                else if (ReenterScene.lastTransitionType == typeof(HoleTeleporter)) HoleTeleporter.fallingDownOnHole = true;
                else if (ReenterScene.lastTransitionType == typeof(Elevator)) Elevator.ridingElevator = true;

                MapManager mapManager = PseudoSingleton<MapManager>.instance;
                mapManager.LoadRoom(SceneManager.GetActiveScene().name, transition);
            }
        }
    }

    [HarmonyPatch(typeof(MapManager), nameof(MapManager.LoadPlayerRoom)), HarmonyPrefix]
    public static void RecordLastTransitionTypeMapManager()
    {
        ReenterScene.lastLadder = null;
        ReenterScene.lastCraterTowerElevator = null;

        if (ScreenTransition.playerTransitioningScreens) ReenterScene.lastTransitionType = typeof(ScreenTransition);
        else if (HoleTeleporter.fallingDownOnHole) ReenterScene.lastTransitionType = typeof(HoleTeleporter);
        else if (Elevator.ridingElevator) ReenterScene.lastTransitionType = typeof(Elevator);
        else ReenterScene.lastTransitionType = null;
    }

    [HarmonyPatch(typeof(SceneManager), "LoadSceneAsyncNameIndexInternal"), HarmonyPrefix]
    public static void RecordLastTransitionTypeSceneManager()
    {
        if (!string.IsNullOrEmpty(SceneChangeLadder.currentLadder))
        {
            ReenterScene.lastTransitionType = typeof(SceneChangeLadder);
            ReenterScene.lastLadder = SceneChangeLadder.currentLadder;
        }
        else if (!string.IsNullOrEmpty(CraterTowerElevator.currentElevator))
        {
            ReenterScene.lastTransitionType = typeof(CraterTowerElevator);
            ReenterScene.lastCraterTowerElevator = CraterTowerElevator.currentElevator;
        }
    }


}
