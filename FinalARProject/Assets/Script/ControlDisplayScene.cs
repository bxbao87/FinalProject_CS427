using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class ControlDisplayScene : MonoBehaviour
{
    public string Name;
    public Transform ModelWindow;
    public GameObject TextObject;
    
    public static UnityEngine.Object LoadPrefabFromFile(string filename){
      Debug.Log("Trying to load LevelPrefab from file ("+filename+ ")...");
      var loadedObject = Resources.Load(filename);
      if (loadedObject == null){
          throw new FileNotFoundException("...no file found - please check the configuration");
      }
      return loadedObject;
    }

    Dictionary<string, Vector3> scale = new Dictionary<string, Vector3>();
    Dictionary<string, Vector3> Position = new Dictionary<string, Vector3>();
    GameObject newObject;
    void Start(){
        setup_dict();
        var loadedPrefabResource = LoadPrefabFromFile(Name);
        newObject = Instantiate(loadedPrefabResource,ModelWindow) as GameObject;
        newObject.transform.localScale = scale[Name];    
        newObject.transform.localPosition = Position[Name];

        /////////////////////////////////////////////////////////

        string ReadFromFilePath = Application.streamingAssetsPath + "/Animal_Information/" + Name + ".txt";
        List<string> filelines = File.ReadAllLines(ReadFromFilePath).ToList();

        TextObject.GetComponent<Text>().text = ""; // reset text objet
        foreach(string line in filelines){
            string s = TextObject.GetComponent<Text>().text;
            TextObject.GetComponent<Text>().text = s + '\n' + line;
        }

        ////////////////////////////////////////////////////////////

    }
    // Update is called once per frame
    void Update()
    {
        newObject.transform.Rotate(new Vector3(0, 0.5f, 0));
    }

    void setup_dict(){
        scale.Add(      "Wolf"                  , new Vector3(300, 300, 300));
        Position.Add(   "Wolf"                  , new Vector3(0, 0, 0));
        
        scale.Add(      "Stag"                  , new Vector3(70, 70, 90));
        Position.Add(   "Stag"                  , new Vector3(0, 0, 0));

        scale.Add(      "Hare"                  , new Vector3(500, 500, 500));
        Position.Add(   "Hare"                  , new Vector3(0, 0, 0));

        scale.Add(      "Rhino"                 , new Vector3(200, 200, 200));
        Position.Add(   "Rhino"                 , new Vector3(0, 0, 0));

        scale.Add(      "AfricanGiraffe"        , new Vector3(60, 60, 60));
        Position.Add(   "AfricanGiraffe"        , new Vector3(0, -30, 0));

        scale.Add(      "Spider"                , new Vector3(20, 20, 20));
        Position.Add(   "Spider"                , new Vector3(0, 30, 0));

        scale.Add(      "Tiger"                 , new Vector3(60, 60, 60));
        Position.Add(   "Tiger"                 , new Vector3(50, 50, -100));
    }
}
