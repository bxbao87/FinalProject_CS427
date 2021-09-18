using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryAudioSource : MonoBehaviour
{
    public string isplay;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (isplay == "True"){
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
