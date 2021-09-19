using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    Button musicButton, soundButton;
    [SerializeField]
    Sprite onSpr, offSpr;

    string soundOn = "True";

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
        musicButton.image.sprite = offSpr;
    }

    public void play_music()
    {
        this.music_play = "True";
        if (this.music.isPlaying == false)
            this.music.Play();
        PlayerPrefs.SetString(Constant.prefMusic, this.music_play);
        musicButton.image.sprite = onSpr;
    }

    public void switchMusic()
    {
        if (this.music_play == "True")
        {
            this.music_play = "False";
            this.music.Stop();
            musicButton.image.sprite = offSpr;
        }
        else
        {
            this.music_play = "True";
            if (this.music.isPlaying == false)
                this.music.Play();
            musicButton.image.sprite = onSpr;
        }
        PlayerPrefs.SetString(Constant.prefMusic, this.music_play);
    }

    public void switchSound()
    {
        if (soundOn == "True")
        {
            soundOn = "False";
            soundButton.image.sprite = offSpr;
        }
        else
        {
            soundOn = "True";
            soundButton.image.sprite = onSpr;
        }
        PlayerPrefs.SetString(Constant.prefSound, soundOn);
    }

    //public void stop_sound()
    //{
    //    PlayerPrefs.SetString(Constant.prefSound, "False");
    //    soundButton.image.sprite = offSpr;
    //}

    //public void play_sound()
    //{
    //    PlayerPrefs.SetString(Constant.prefSound, "True");
    //    soundButton.image.sprite = onSpr;
    //}

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
        }
        else
        {
            this.camera.SetActive(false);
            this.title.SetActive(false);
            this.background.SetActive(false);
            this.buttons.SetActive(false);
            if (scene.name == Constant.multitrackScene)// || scene.name == Constant.gamePlayScene)
            {
                Debug.Log("enter start");
                if (this.music_play == "True") 
                    play_music();
                return;
            }
            else
            {
                this.music.Stop();
            }

        }


    }

}
