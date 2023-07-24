using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.cheats;

public class Debug
{

    public static void SetDebug(bool debug)
    {
        DebugModeDetector.debugMode = debug;
    }

}
