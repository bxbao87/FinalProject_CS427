using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryAudioSource : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        string isPlay = PlayerPrefs.GetString(Constant.prefMusic, "True");
        if (isPlay == "True"){
            audioSource.Play();
            audioSource.loop = true;
        }
        else
        {
            audioSource.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
