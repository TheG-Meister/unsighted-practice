using dev.gmeister.unsighted.practice.unsighted;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using dev.gmeister.unsighted.practice.core;

namespace dev.gmeister.unsighted.practice.cheats;

public class QuickIO
{

    private State[] states;

    public QuickIO(int capacity)
    { 
        this.states = new State[capacity];

        for (int i = 0; i < this.states.Length; i++)
        {
            string path = Constants.STATE_PATH + $"state-{i + 1}.dat";
            if (File.Exists(path))
            {
                this.states[i] = Serializer.Load<State>(path);
            }
        }
    }

    public void QuickSave(int stateSlot)
    {
        State state = State.Create();
        this.states[stateSlot] = state;
        if (!Directory.Exists(Constants.STATE_PATH)) Directory.CreateDirectory(Constants.STATE_PATH);
        Serializer.Save(Constants.STATE_PATH + $"state-{stateSlot + 1}.dat", state);
    }

    public void QuickLoad(int stateSlot)
    {
        if (stateSlot < this.states.Length && this.states[stateSlot] != null)
        {
            GlobalGameData data = PseudoSingleton<GlobalGameData>.instance;
            State stateClone = State.DeepClone(this.states[stateSlot]);

            data.currentData.playerDataSlots[data.loadedSlot] = stateClone.data;

            LevelController.acidTubesInfo = stateClone.acidTubesInfo.Select(info => info.ToAcidTubeInfo()).ToList();
            LevelController.deceasedEnemiesInfo = stateClone.deceasedEnemiesInfo;
            LevelController.dropInfo = stateClone.dropInfo.Select(info => info.ToDropInfo()).ToList();
        }
    }
}
