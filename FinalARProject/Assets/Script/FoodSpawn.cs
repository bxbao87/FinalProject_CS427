using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class FoodSpawn : MonoBehaviour
{
    public GameObject foodObject;
    private GameObject curObject;

    [SerializeField]
    GameObject reticlePrefab;
    private GameObject reticle;
    public PlaneStopTracking planeStopTrackingScript;
    public int foodEatten = 30;

    [SerializeField]
    Text text;

    [SerializeField]
    TimerCountDown timerCountDown;

    [SerializeField]
    float scaleFactor = 0.09f;

    void Start()
    {
        string modelName = PlayerPrefs.GetString("animal", Constant.foodChainCommon);
        foodObject = (GameObject)ControlDisplayScene.LoadPrefabFromFile(Constant.models[modelName]);
        text.text = foodEatten.ToString();
        curObject = null;
    }

    void Update()
    {
        var standPlane = planeStopTrackingScript.standPlane;
        if (standPlane != null)
        {
            if (curObject == null)
            {
                spawnFood(standPlane);
            }
            else if (!curObject.activeSelf)
            {
                curObject.SetActive(true);
                foodEatten--;
                text.text = foodEatten.ToString();
                if (foodEatten == 0)
                {
                    timerCountDown.gameCompleted();
                    return;
                }
                Vector3 newPos = randomPosition(standPlane);
                curObject.transform.position = newPos;
                newPos.y += 3;
                reticle.transform.position = newPos;
            }

            var foodPosition = curObject.transform.position;
            foodPosition.Set(foodPosition.x, standPlane.center.y, foodPosition.z);
        }
    }

    public static Vector3 randomPointInTriangle(Vector3 u, Vector3 v)
    {
        float a = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        if (b + a > 1)
        {
            b = 1 - b;
            a = 1 - a;
        }

        return (u * a) + (v * b);
    }

    public static Vector3 randomPosition(ARPlane plane)
    {
        var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;

        var triangles = mesh.triangles;
        var triangle = triangles[Random.Range(0, triangles.Length - 1)] / 3 * 3;

        var vertices = mesh.vertices;
        var randomPoint = randomPointInTriangle(vertices[triangle], vertices[triangle + 1]);

        var ret = plane.transform.TransformPoint(randomPoint);
        return ret;
    }

    public void spawnFood(ARPlane plane)
    {
        var tmp = GameObject.Instantiate(foodObject);
        Vector3 scaleVec = tmp.transform.localScale;
        scaleVec *= scaleFactor;
        tmp.transform.localScale = scaleVec;
        Vector3 newPos = randomPosition(plane);
        tmp.transform.position = newPos;
        curObject = tmp;

        var reti = GameObject.Instantiate(reticlePrefab);
        newPos.y += 3;
        reti.transform.position = newPos;
        reti.transform.localScale = reti.transform.localScale * scaleFactor;
        reticle = reti;
    }

}
