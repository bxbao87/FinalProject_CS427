using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TimerCountDown : MonoBehaviour
{
    public float duration = 60f;
    float timeLeft = 0f;
    [SerializeField] Text countDownText;

    [SerializeField]
    GameObject canvasPause;

    [SerializeField]
    GameObject canvasGameOver;

    [SerializeField]
    GameObject pauseBtn;

    [SerializeField]
    PlaneStopTracking planeStopTracking;

    public AudioSource audioSource;
    [SerializeField]
    AudioClip victoryClip, loseClip;
    private AudioClip themeClip;
    private bool isMusicMute = true;

    void Start()
    {
        Time.timeScale = 1;
        timeLeft = duration;
        countDownText.text = timeLeft.ToString("0");
        isMusicMute = PlayerPrefs.GetString(Constant.prefMusic, "True") == "True" ? false : true;

        string Name = PlayerPrefs.GetString(Constant.prefAnimal, Constant.foodChainCommon);
        if (!isMusicMute)
        {
            themeClip = Resources.Load<AudioClip>("Sound/" + Name);
            if (themeClip == null) Debug.Log("null clip");
            if (themeClip != null)
            {
                audioSource.clip = themeClip;
                audioSource.Play();
                audioSource.loop = true;
            }
        }
        onPauseGame();
    }

    void Update()
    {
        if (planeStopTracking.standPlane == null)
            return;

        timeLeft -= 1 * Time.deltaTime;
        if (timeLeft < 0)
        {
            gameOver();
            timeLeft = 0;
        }
        countDownText.text = timeLeft.ToString("0");
    }

    private void onPauseGame()
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        planeStopTracking.planeManager.enabled = false;
        planeStopTracking.enabled = false;
    }

    public void pauseGame()
    {
        onPauseGame();
        canvasPause.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        canvasPause.SetActive(false);
        pauseBtn.SetActive(true);
        planeStopTracking.planeManager.enabled = true;
        planeStopTracking.enabled = true;
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        //audioSource.PlayOneShot(themeClip, 1f);
        SceneManager.LoadScene("GamePlay"); // reload current scene
    }

    public void backMenu()
    {
        Time.timeScale = 1;
        audioSource.Stop();
        loadScene(Constant.menu);
    }

    public void gameOver()
    {
        Time.timeScale = 0;
        canvasGameOver.SetActive(true);
        if(!isMusicMute)
            audioSource.PlayOneShot(loseClip, 1f);
    }

    public void gameCompleted()
    {
        Time.timeScale = 0;
        canvasGameOver.SetActive(true);
        Text gameOver = canvasGameOver.transform.Find("GameOver").GetComponent<Text>();
        gameOver.text = Constant.gameCompleted;
        if (!isMusicMute) 
            audioSource.PlayOneShot(victoryClip, 1f);
    }

    private void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
