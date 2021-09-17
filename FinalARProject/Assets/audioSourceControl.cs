using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSourceControl : MonoBehaviour
{
    // Start is called before the first frame update
    static audioSourceControl Player_Instance;
    public AudioSource music;
    public string music_play;
    void Start()
    {
        if (audioSourceControl.Player_Instance == null)
        {
            
            //dont destroy this instance
            DontDestroyOnLoad(this);
            
            //save a reference to this instance in the static variable
            audioSourceControl.Player_Instance = this;

            //track scene changes
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void stop_music(){
        this.music_play = "False";
        this.music.Stop();
    }
    public void play_music(){
        this.music_play = "True";
        if (this.music.isPlaying == false)
            this.music.Play();
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode load_scene_mode)
    {
        if (scene.name == "Nhan_Menu_Shop")
        {
            this.music.Stop();
        }
        else
        {
            if (this.music_play == "True") play_music();
        }
    }

}
