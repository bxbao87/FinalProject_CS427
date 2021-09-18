using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceMaskChange : MonoBehaviour
{

    [SerializeField]
    ARFaceManager faceManager;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        string Name = "AR " + PlayerPrefs.GetString(Constant.prefAnimal, Constant.foodChainCommon);
        var loadedPrefabResource = ControlDisplayScene.LoadPrefabFromFile(Name);
        faceManager.facePrefab = Instantiate(loadedPrefabResource) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
