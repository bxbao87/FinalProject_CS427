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
                if(sceneName == menu)
                {
                    Application.Quit();
                }
            }
        }
    }

    public void backBtn(string scene)
    {
        if (scene == null || scene == "")
            scene = Constant.menu;
        SceneManager.LoadScene(scene);
    }
}
