using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public static Dictionary<string, string> models = new Dictionary<string, string>() {
        {"Wolf", "Hare" },
        {"Hare", "Bush" },
        {"Stag", "Bush" },
        {"Tiger", "Hare" },
        {"Rhino", "Bush" },
        {"Spider", "Dragonfly" },
        {"AfricanGiraffe", "Bush" }
    };

    public static string foodChainCommon = "Hare";
    public static string gameCompleted = "GAME COMPLETED";


}
