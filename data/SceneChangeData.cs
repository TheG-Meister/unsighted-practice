using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dev.gmeister.unsighted.practice.data;

[Serializable]
public class SceneChangeData
{

    [Serializable]
    public class PlayerSceneChangeData
    {
        public readonly bool running;
        public readonly bool runningWithDoubleTap;
        public readonly bool spinner;
        public readonly bool mecha;

        public PlayerSceneChangeData(bool running, bool runningWithDoubleTap, bool spinner, bool mecha)
        {
            this.running = running;
            this.runningWithDoubleTap = runningWithDoubleTap;
            this.spinner = spinner;
            this.mecha = mecha;
        }

        public PlayerSceneChangeData(PlayerSceneChangeData other) : this(other.running, other.runningWithDoubleTap, other.spinner, other.mecha)
        { }
    }

    public string scene;
    public Type transitionType;
    public string transitionObject;
    public string lastScene;
    public Vector3 screenTransitionDirection = default;
    public Vector3 screenTransitionPlayerDirection = default;
    public List<PlayerSceneChangeData> playerSceneChangeData = null;

    public SceneChangeData()
    {
    }

    public SceneChangeData(string scene, Type transitionType, string transitionObject = null, string lastScene = null, Vector3 screenTransitionDirection = default, Vector3 screenTransitionPlayerDirection = default, params PlayerSceneChangeData[] playerSceneChangeData)
    {
        this.scene = scene;
        this.transitionType = transitionType;
        this.transitionObject = transitionObject;
        this.lastScene = lastScene;
        this.screenTransitionDirection = screenTransitionDirection;
        this.screenTransitionPlayerDirection = screenTransitionPlayerDirection;
        this.playerSceneChangeData = new(playerSceneChangeData);
    }

    public SceneChangeData(SceneChangeData other)
    {
        this.scene = other.scene;
        this.transitionType = other.transitionType;
        this.transitionObject = other.transitionObject;
        this.lastScene = other.lastScene;
        this.screenTransitionDirection = other.screenTransitionDirection;
        this.screenTransitionPlayerDirection = other.screenTransitionPlayerDirection;
        this.playerSceneChangeData = new();
        if (other.playerSceneChangeData != null) foreach (PlayerSceneChangeData data in other.playerSceneChangeData) this.playerSceneChangeData.Add(new(data));
    }

}
