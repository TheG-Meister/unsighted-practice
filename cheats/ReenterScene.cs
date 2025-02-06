using dev.gmeister.unsighted.practice.core;
using dev.gmeister.unsighted.practice.data;
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

    public static SceneChangeData sceneChangeData = null;

    public static void Respawn(bool transition)
    {
        if (!PlayerInfo.cutscene && PlayerInfo.AtLeastOnePlayerAlive() && !gameTime.paused)
        {
            PlayerData data = PseudoSingleton<Helpers>.instance.GetPlayerData();
            PlayerInfo.cutscene = true;
            LevelController.restartingPlayer = true;
            if (data.lastTerminalData == null || string.IsNullOrEmpty(data.lastTerminalData.sceneName))
            {
                PseudoSingleton<MapManager>.instance.LoadRoom(data.lastCheckpoint.sceneName, transition);
            }
            else
            {
                PseudoSingleton<MapManager>.instance.LoadRoom(data.lastTerminalData.sceneName, transition);
            }
        }
    }

    public static void Reenter(bool transition)
    {
        Reenter(sceneChangeData, transition);
    }

    public static void Reenter(SceneChangeData sceneChangeData, bool transition)
    {
        if (!PlayerInfo.cutscene && PlayerInfo.AtLeastOnePlayerAlive() && !gameTime.paused)
        {
            if (sceneChangeData == null) ReenterScene.Respawn(transition);
            else
            {
                PlayerInfo.cutscene = true;

                if (sceneChangeData.transitionType == typeof(ScreenTransition))
                {
                    ScreenTransition.lastSceneName = sceneChangeData.lastScene;
                    ScreenTransition.currentDoorName = sceneChangeData.transitionObject;
                    ScreenTransition.direction = sceneChangeData.screenTransitionDirection;
                    ScreenTransition.directionPlayerCamerFrom = Helpers.Get8AxisDirection(sceneChangeData.screenTransitionPlayerDirection);
                    ScreenTransition.playerTransitioningScreens = true;
                }
                else if (sceneChangeData.transitionType == typeof(SceneChangeLadder)) SceneChangeLadder.currentLadder = sceneChangeData.transitionObject;
                else if (sceneChangeData.transitionType == typeof(CraterTowerElevator)) CraterTowerElevator.currentElevator = sceneChangeData.transitionObject;
                else if (sceneChangeData.transitionType == typeof(HoleTeleporter)) HoleTeleporter.fallingDownOnHole = true;
                else if (sceneChangeData.transitionType == typeof(Elevator)) Elevator.ridingElevator = true;
                else if (sceneChangeData.transitionType == typeof(CrystalTeleportExit)) CrystalTeleportExit.usingCrystalTeleport = true;
                else if (sceneChangeData.transitionType == typeof(Terminal) || sceneChangeData.transitionType == typeof(TemporaryCheckpointLocation)) LevelController.restartingPlayer = true; 

                MapManager mapManager = PseudoSingleton<MapManager>.instance;
                mapManager.LoadRoom(sceneChangeData.scene, transition);
            }
        }
    }

    [HarmonyPatch(typeof(MapManager), nameof(MapManager.LoadPlayerRoom)), HarmonyPrefix]
    public static void RecordLastTransitionTypeMapManager(string nextSceneName)
    {
        string scene = nextSceneName;

        if (ScreenTransition.playerTransitioningScreens)
        {
            sceneChangeData = new(scene, typeof(ScreenTransition), ScreenTransition.currentDoorName, ScreenTransition.lastSceneName, ScreenTransition.direction, Helpers.AxisDirectionToVector3(ScreenTransition.directionPlayerCamerFrom));
        }
        else if (HoleTeleporter.fallingDownOnHole) sceneChangeData = new(scene, typeof(HoleTeleporter));
        else if (Elevator.ridingElevator) sceneChangeData = new(scene, typeof(Elevator));
        else if (CrystalTeleportExit.usingCrystalTeleport) sceneChangeData = new(scene, typeof(CrystalTeleportExit));
        else sceneChangeData = new(scene, null);
    }

    [HarmonyPatch(typeof(SceneManager), "LoadSceneAsyncNameIndexInternal"), HarmonyPrefix]
    public static void RecordLastTransitionTypeSceneManager(string sceneName)
    {
        string scene = sceneName;
        Helpers helpers = PseudoSingleton<Helpers>.instance;
        GlobalGameData gameData = PseudoSingleton<GlobalGameData>.instance;

        if (!string.IsNullOrEmpty(SceneChangeLadder.currentLadder))
        {
            sceneChangeData = new(scene, typeof(SceneChangeLadder), SceneChangeLadder.currentLadder);
        }
        else if (!string.IsNullOrEmpty(CraterTowerElevator.currentElevator))
        {
            sceneChangeData = new(scene, typeof(CraterTowerElevator), CraterTowerElevator.currentElevator);
        }
        else if (LevelController.restartingPlayer && helpers != null && gameData != null)
        {
            PlayerData data = helpers.GetPlayerData();
            if (data.lastTerminalData == null || string.IsNullOrEmpty(data.lastTerminalData.areaName))
            {
                sceneChangeData = new(scene, typeof(TemporaryCheckpointLocation));
            }
            else
            {
                sceneChangeData = new(scene, typeof(Terminal));
            }
        }
    }

}
