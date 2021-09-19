using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceMaskChange : MonoBehaviour
{

    [SerializeField]
    ARFaceManager faceManager;

    public AudioSource audioSource;
    public string sound;
    AudioClip clip;
    float timer = 0.0f;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        string Name = PlayerPrefs.GetString(Constant.prefAnimal, Constant.foodChainCommon);
        var loadedPrefabResource = ControlDisplayScene.LoadPrefabFromFile("AR " + Name);
        faceManager.facePrefab = Instantiate(loadedPrefabResource) as GameObject;

        clip = Resources.Load<AudioClip>("Sound/" + Name);
        if (clip == null) Debug.Log("null clip");
        if (clip != null && sound == "True") audioSource.PlayOneShot(clip, 1.0f);
    }

    private void Update()
    {
        if (clip != null)
        {
            timer += Time.deltaTime;

            if (timer > clip.length - 2 && sound == "True")
            {
                audioSource.PlayOneShot(clip, 1.0f);
                timer = 0;
            }
        }
    }

}
