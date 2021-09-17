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

    void Start()
    {
        timeLeft = duration;
        countDownText.text = timeLeft.ToString("0");
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

    public void pauseGame()
    {
        Time.timeScale = 0; 
        canvasPause.SetActive(true);
        pauseBtn.SetActive(false);
        planeStopTracking.planeManager.enabled = false;
        planeStopTracking.enabled = false;
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
        loadScene("GamePlay");
    }

    public void backMenu()
    {
        Time.timeScale = 1;
        loadScene(Constant.menu);
    }

    public void gameOver()
    {
        Time.timeScale = 0;
        canvasGameOver.SetActive(true);
    }

    public void gameCompleted()
    {
        Time.timeScale = 0;
        canvasGameOver.SetActive(true);
        Text gameOver = canvasGameOver.transform.Find("GameOver").GetComponent<Text>();
        gameOver.text = Constant.gameCompleted;
    }

    private void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
