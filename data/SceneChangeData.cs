using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace dev.gmeister.unsighted.practice.data;

[Serializable]
public class SceneChangeData
{

    public string scene;
    public Type transitionType;
    public string transitionObject;
    public string lastScene;

    public SceneChangeData()
    {
    }

    public SceneChangeData(string scene, Type transitionType, string transitionObject = null, string lastScene = null)
    {
        this.scene = scene;
        this.transitionType = transitionType;
        this.transitionObject = transitionObject;
        this.lastScene = lastScene;
    }

    public SceneChangeData(SceneChangeData other)
    {
        this.scene = other.scene;
        this.transitionType = other.transitionType;
        this.transitionObject = other.transitionObject;
        this.lastScene = other.lastScene;
    }

}
