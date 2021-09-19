using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceMaskChange : MonoBehaviour
{

    [SerializeField]
    ARFaceManager faceManager;

    public AudioSource audioSource;
    AudioClip clip = null;
    float timer = 0.0f;

    private bool isSoundMute = false;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        string Name = PlayerPrefs.GetString(Constant.prefAnimal, Constant.foodChainCommon);
        var loadedPrefabResource = ControlDisplayScene.LoadPrefabFromFile("AR " + Name);
        faceManager.facePrefab = Instantiate(loadedPrefabResource) as GameObject;


        isSoundMute = PlayerPrefs.GetString(Constant.prefSound, "True") == "True" ? false : true;
        if (!isSoundMute)
        {
            clip = Resources.Load<AudioClip>("Sound/" + Name);
            if (clip == null) Debug.Log("null clip");
            if (clip != null)
            {
                audioSource.PlayOneShot(clip, 1.0f);
                audioSource.loop = true;
            }
        }
    }

    private void Update()
    {
        if (clip != null)
        {
            timer += Time.deltaTime;

            if (timer > clip.length - 2)
            {
                audioSource.PlayOneShot(clip, 1.0f);
                timer = 0;
            }
        }
    }

}
