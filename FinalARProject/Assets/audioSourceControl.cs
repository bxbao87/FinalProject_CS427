using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSourceControl : MonoBehaviour
{
    // Start is called before the first frame update
    static audioSourceControl Player_Instance;
    public AudioSource music;
    public string music_play;
    public GameObject camera;
    public GameObject title;
    public GameObject background;
    public GameObject buttons;

    private string trueVal = "True", falseVal = "False";
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

    public void stop_music()
    {
        this.music_play = "False";
        this.music.Stop();
        PlayerPrefs.SetString(Constant.prefMusic, this.music_play);
    }

    public void play_music()
    {
        this.music_play = "True";
        if (this.music.isPlaying == false)
            this.music.Play();
        PlayerPrefs.SetString(Constant.prefMusic, this.music_play);
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode load_scene_mode)
    {

        if (scene.name == "Main Menu")
        {
            Debug.Log("enter Main Menu");
            if (this.music_play == "True") play_music();
            this.camera.SetActive(true);
            this.title.SetActive(true);
            this.background.SetActive(true);
            this.buttons.SetActive(true);
            return;
        }
        this.camera.SetActive(false);
        this.title.SetActive(false);
        this.background.SetActive(false);
        this.buttons.SetActive(false);
        if (scene.name == "Nhan_Menu_Shop")
        {
            this.music.Stop();
            return;
        }

        if (scene.name == "Multi_Track")
        {
            Debug.Log("enter start");
            return;
        }



    }

}
