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

public class QuickSaves
{

    private string dir;
    private State[] states;

    public QuickSaves(int capacity, string dir = Constants.QUICKSAVES_PATH)
    { 
        this.states = new State[capacity];
        this.dir = dir;

        for (int i = 0; i < this.states.Length; i++)
        {
            string path = this.dir + $"state-{i + 1}.dat";
            if (File.Exists(path))
            {
                this.states[i] = State.Read(path);
            }
        }
    }

    public void QuickSave(int stateSlot)
    {
        State state = State.Create();
        this.states[stateSlot] = state;
        if (!Directory.Exists(this.dir)) Directory.CreateDirectory(this.dir);
        State.Write(this.dir + $"state-{stateSlot + 1}.dat", state);
    }

    public void QuickLoad(int stateSlot)
    {
        if (stateSlot < this.states.Length && this.states[stateSlot] != null) State.Load(this.states[stateSlot]);
    }

}
