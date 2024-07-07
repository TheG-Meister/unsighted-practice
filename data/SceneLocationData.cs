using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dev.gmeister.unsighted.practice.data;

[Serializable]
public class SceneLocationData
{

    public string scene;
    public Vector3 p1pos;
    public Vector3 p2pos;

    public SceneLocationData()
    {
    }

    public SceneLocationData(string scene, Vector3 p1pos, Vector3 p2pos)
    {
        this.scene = scene;
        this.p1pos = p1pos;
        this.p2pos = p2pos;
    }

    public static SceneLocationData Create()
    {
        string scene = SceneManager.GetActiveScene().name;

        List<PlayerInfo> players = PseudoSingleton<PlayersManager>.instance.players;
        Vector3 p1pos = players[0].myCharacter.gameObject.transform.position;
        p1pos.z = players[0].myCharacter.myPhysics.globalHeight;

        Vector3 p2pos;
        if (players.Count > 1)
        {
            p2pos = players[1].myCharacter.gameObject.transform.position;
            p2pos.z = players[1].myCharacter.myPhysics.globalHeight;
        }
        else p2pos = p1pos;

        return new SceneLocationData(scene, p1pos, p2pos);
    }

}
