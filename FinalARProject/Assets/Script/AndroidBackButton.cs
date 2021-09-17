using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidBackButton : MonoBehaviour
{
    string sceneName;
    string menu;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        menu = Constant.menu;
    }
    
    void Update()
    {
        // on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(sceneName != menu)
                {
                    backBtn();
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }

    public void backBtn()
    {
        string scene = PlayerPrefs.GetString(Constant.prefPrevScene, menu);
        SceneManager.LoadScene(scene);
    }
}
