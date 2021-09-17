using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTap : MonoBehaviour, IPointerDownHandler
{
    private string detail, filter, play, prefAnimal;

    private void Start()
    {
        detail = Constant.detail;
        filter = Constant.filter;
        play = Constant.play;
        prefAnimal = Constant.prefAnimal;
        addPhysicsRaycaster();
    }


    private void loadScene(string sceneName)
    {
        PlayerPrefs.SetString(Constant.prefPrevScene, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }

    void addPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonClickHandler(eventData.pointerCurrentRaycast.gameObject);
    }


    public void buttonClickHandler(GameObject obj)
    {
        Transform par = obj.transform.parent;
        if (par != null)
        {
            par = par.parent;

            if (par != null)
            {
                string objTag = obj.transform.tag;
                string objName = par.name;

                PlayerPrefs.SetString(prefAnimal, objName);

                if (objTag == detail)
                {
                    loadScene("Display Info");
                }
                else if (objTag == filter)
                {

                }
                else if (objTag == play)
                {
                    loadScene("GamePlay");
                }
            }
        }
    }

}
