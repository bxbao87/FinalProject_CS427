using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class ControlDisplayScene : MonoBehaviour
{
    public string _Name;
    public Transform ModelWindow;
    public GameObject TextObject;
    public AudioSource audioSource;
    public string Music;
    AudioClip clip;
    float timer = 0.0f;
    
    public static UnityEngine.Object LoadPrefabFromFile(string filename){
    //   Debug.Log("Trying to load LevelPrefab from file ("+filename+ ")...");
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
        
        string Name = PlayerPrefs.GetString(Constant.prefAnimal, Constant.foodChainCommon);
        var loadedPrefabResource = LoadPrefabFromFile(Name);
        newObject = Instantiate(loadedPrefabResource,ModelWindow) as GameObject;
        newObject.transform.localScale = scale[Name] * 0.5f;    
        newObject.transform.localPosition = Position[Name];

        /////////////////////////////////////////////////////////
        var filelines = Resources.Load<TextAsset>("Animal_infomation/"+Name).ToString();

        TextObject.GetComponent<Text>().text = filelines; // reset text objet


        ////////////////////////////////////////////////////////////
        clip = Resources.Load<AudioClip>("Sound/" + Name);
        if (clip == null) Debug.Log("null clip");
        if (clip != null && Music == "True") audioSource.PlayOneShot(clip,1.0f);
        

        ////////////////////////////////////////////////////////////
        
    }
    // Update is called once per frame
    void Update()
    {
        newObject.transform.Rotate(new Vector3(0, 0.5f, 0));

        if (clip != null) {
            timer += Time.deltaTime;

            if (timer > clip.length-2 && Music == "True"){
                audioSource.PlayOneShot(clip,1.0f);
                timer = 0;
            }
        }


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
