using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dev.gmeister.unsighted.practice.cheats;

[Harmony]
public class TimeCheat
{

    private bool paused;
    public int updates;

    public TimeCheat()
    {
        this.paused = false;
        this.updates = 0;
    }

    public void Apply()
    {
        if (this.paused)
        {
            if (this.updates > 0)
            {
                this.updates--;
                Time.timeScale = 1f;
            }
            else Time.timeScale = 0f;
        }
    }

    public void Toggle()
    {
        this.Pause(!this.paused);
    }

    public void Pause(bool pause)
    {
        this.paused = pause;
        if (!this.paused) Time.timeScale = 1f;
    }

}
