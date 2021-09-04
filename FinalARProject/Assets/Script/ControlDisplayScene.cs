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
    
    private UnityEngine.Object LoadPrefabFromFile(string filename){
      Debug.Log("Trying to load LevelPrefab from file ("+filename+ ")...");
      var loadedObject = Resources.Load(filename);
      if (loadedObject == null){
          throw new FileNotFoundException("...no file found - please check the configuration");
      }
      return loadedObject;
    }

    void Start(){

    Debug.Log(Name);
    var loadedPrefabResource = LoadPrefabFromFile(Name);
    GameObject newObject = Instantiate(loadedPrefabResource,ModelWindow) as GameObject;
    newObject.transform.localScale = new Vector3(300, 300, 300);    

    /////////////////////////////////////////////////////////

    string ReadFromFilePath = Application.streamingAssetsPath + "/Animal_Information/" + Name + ".txt";
    List<string> filelines = File.ReadAllLines(ReadFromFilePath).ToList();

    TextObject.GetComponent<Text>().text = ""; // reset text objet
    foreach(string line in filelines){
        string s = TextObject.GetComponent<Text>().text;
        TextObject.GetComponent<Text>().text = s + '\n' + line;
    }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
