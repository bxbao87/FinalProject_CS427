using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public void Exit(){
        Application.Quit();
    }

    public void GalleryScene(string sceneName)
    {
        if (sceneName == null || sceneName == "")
            sceneName = "Nhan_Menu_Shop";
        loadScene(sceneName);
    }

    public void StartScene(string sceneName)
    {
        if (sceneName == null || sceneName == "")
            sceneName = "Multi_Track";
        loadScene(sceneName);
    }

    public void loadScene(string sceneName)
    {
        PlayerPrefs.SetString(Constant.prefPrevScene, Constant.menu);
        SceneManager.LoadScene(sceneName);
    }
}
