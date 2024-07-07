using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.gmeister.unsighted.practice.cheats;

public class DataStrings
{

    public const string PROLOGUE_OVER = "FinishedFirstPart";
    public const string IRIS_PRESENT = "Iris";
    public const string IRIS_RESCUED = "RescuedIris";

    public static void ClearDataStrings()
    {
        List<string> dataStrings = PseudoSingleton<Helpers>.instance.GetPlayerData().dataStrings;
        List<string> preserved = dataStrings.FindAll(s =>
        {
            return s switch
            {
                PROLOGUE_OVER or IRIS_PRESENT or IRIS_RESCUED => true,
                _ => false,
            };
        });

        dataStrings.Clear();
        dataStrings.AddRange(preserved);
    }

    public static void SetDataString(string dataString, bool condition)
    {
        List<string> dataStrings = PseudoSingleton<Helpers>.instance.GetPlayerData().dataStrings;

        if (condition && !dataStrings.Contains(dataString)) dataStrings.Add(dataString);
        else if (!condition && dataStrings.Contains(dataString)) dataStrings.Remove(dataString);
    }

    public static void SetPrologue(bool prologue) => SetDataString(PROLOGUE_OVER, !prologue);

    public static void SetIris(bool iris)
    {
        SetDataString(IRIS_PRESENT, iris);
        SetDataString(IRIS_RESCUED, iris);
    }

}
