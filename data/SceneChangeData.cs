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

    public string scene;
    public Type transitionType;
    public string transitionObject;
    public string lastScene;
    public Vector3 screenTransitionDirection = default;
    public Vector3 screenTransitionPlayerDirection = default;

    public SceneChangeData()
    {
    }

    public SceneChangeData(string scene, Type transitionType, string transitionObject = null, string lastScene = null, Vector3 screenTransitionDirection = default, Vector3 screenTransitionPlayerDirection = default)
    {
        this.scene = scene;
        this.transitionType = transitionType;
        this.transitionObject = transitionObject;
        this.lastScene = lastScene;
        this.screenTransitionDirection = screenTransitionDirection;
        this.screenTransitionPlayerDirection = screenTransitionPlayerDirection;
    }

    public SceneChangeData(SceneChangeData other)
    {
        this.scene = other.scene;
        this.transitionType = other.transitionType;
        this.transitionObject = other.transitionObject;
        this.lastScene = other.lastScene;
        this.screenTransitionDirection = other.screenTransitionDirection;
        this.screenTransitionPlayerDirection = other.screenTransitionPlayerDirection;
    }

}
