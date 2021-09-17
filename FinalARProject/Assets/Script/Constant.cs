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

    public const string foodChainCommon = "Hare";
    public const string gameCompleted = "GAME COMPLETED";

    public const string filter = "filter";
    public const string detail = "detail";
    public const string play = "play";

    public const string prefAnimal = "animal";
}
