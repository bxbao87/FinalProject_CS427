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
    AudioClip clip = null;
    float timer = 0.0f;
    bool isSoundMute = false;
    
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
        newObject.transform.localPosition = Position[Name] + new Vector3(20f, 20f, 0f);

        /////////////////////////////////////////////////////////
        var filelines = Resources.Load<TextAsset>("Animal_infomation/"+Name).ToString();

        TextObject.GetComponent<Text>().text = filelines; // reset text objet


        ////////////////////////////////////////////////////////////
        isSoundMute = PlayerPrefs.GetString(Constant.prefSound, "True") == "True" ? false : true;
        if (!isSoundMute)
        {
            clip = Resources.Load<AudioClip>("Sound/" + Name);
            audioSource.loop = true;
            if (clip == null) Debug.Log("null clip");
            if (clip != null) audioSource.PlayOneShot(clip, 1.0f);
            audioSource.loop = true;
        }

        ////////////////////////////////////////////////////////////

    }
    // Update is called once per frame
    void Update()
    {
        newObject.transform.Rotate(new Vector3(0, 0.5f, 0));

        if (clip != null) {
            timer += Time.deltaTime;

            if (timer > clip.length-2){
                audioSource.PlayOneShot(clip,1.0f);
                timer = 0;
            }
        }


    }

    void setup_dict(){
        scale.Add(      "Wolf"                  , new Vector3(290, 290, 290));
        Position.Add(   "Wolf"                  , new Vector3(15, 0, 0));
        
        scale.Add(      "Stag"                  , new Vector3(70, 70, 90));
        Position.Add(   "Stag"                  , new Vector3(5, 0, 0));

        scale.Add(      "Hare"                  , new Vector3(400, 400, 400));
        Position.Add(   "Hare"                  , new Vector3(10, 0, 0));

        scale.Add(      "Rhino"                 , new Vector3(180, 180, 180));
        Position.Add(   "Rhino"                 , new Vector3(20, 0, 20));

        scale.Add(      "AfricanGiraffe"        , new Vector3(60, 60, 60));
        Position.Add(   "AfricanGiraffe"        , new Vector3(10, -30, 0));

        scale.Add(      "Spider"                , new Vector3(15, 15, 15));
        Position.Add(   "Spider"                , new Vector3(5, 30, 0));

        scale.Add(      "Tiger"                 , new Vector3(55, 55, 55));
        Position.Add(   "Tiger"                 , new Vector3(50, 50, -50));
    }
}
