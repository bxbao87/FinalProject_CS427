using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneStopTracking : MonoBehaviour
{
    public ARPlaneManager planeManager;

    [SerializeField]
    ARRaycastManager raycastManager;

    [SerializeField]
    double desiredArea = 1.5;
    private double halfSize = 0;

    public ARPlane standPlane;
    public GameObject playerPrefab;

    private bool initModel = false;

    [SerializeField]
    GameObject infoGroup;

    [SerializeField]
    float scaleFactor = 0.1f;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
        standPlane = null;
        halfSize = Math.Sqrt(desiredArea);
    }

    void Update()
    {
        //if (standPlane == null)
        //{
        //    if (Input.touchCount == 1)
        //    {
        //        var touch = Input.GetTouch(0);
        //        if (touch.phase == TouchPhase.Ended)
        //        {
        //            var hitResults = new List<ARRaycastHit>();
        //            raycastManager.Raycast(touch.position, hitResults, TrackableType.PlaneWithinBounds);

        //            if(hitResults.Count > 0)
        //            {
        //                ARRaycastHit hit = hitResults[0];
        //                standPlane = planeManager.GetPlane(hit.trackableId);

        //                foreach (var plane in planeManager.trackables)
        //                {
        //                    plane.gameObject.SetActive(false);
        //                }

        //                standPlane.gameObject.SetActive(true);
        //                planeManager.planesChanged += disableNewPlanes;
        //            }
        //        }
        //    }
        //}

        if (standPlane != null)
            return;

        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var hitResults = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hitResults, TrackableType.PlaneWithinBounds);
        
        if (hitResults.Count > 0)
        {
            foreach (var hit in hitResults)
            {
                ARPlane plane = planeManager.GetPlane(hit.trackableId);
                if(planeSatisfaction(plane))
                {
                    standPlane = plane.GetComponent<ARPlane>();

                    foreach (var trackplane in planeManager.trackables)
                    {
                        trackplane.gameObject.SetActive(false);
                    }
                    standPlane.gameObject.SetActive(true);
                    planeManager.planesChanged += disableNewPlanes;

                    spawnPlayer(hit.pose.position);
                    infoGroup.SetActive(true);
                    return;
                }
            }


        }

    }

    private bool planeSatisfaction(ARPlane plane)
    {
        return plane.size.x >= halfSize && plane.size.y >= halfSize;

        //return calculatePlaneArea(plane) >= desiredArea;
    }

    private void spawnPlayer(Vector3 pos)
    {
        if (initModel)
            return;
        initModel = true;
        string model = PlayerPrefs.GetString("animal", Constant.foodChainCommon);

        GameObject prefab = (GameObject)ControlDisplayScene.LoadPrefabFromFile(model);

        var obj = GameObject.Instantiate(prefab);
        obj.transform.position = pos;
        Vector3 scaleVec = obj.transform.localScale;
        scaleVec *= scaleFactor;
        obj.transform.localScale = scaleVec;
        
        ObjectMovement movementScript = obj.GetComponent<ObjectMovement>();
        movementScript.enabled = true;
        movementScript.standPlane = standPlane;
        movementScript.raycastManager = raycastManager;
    }

    //private void OnEnable()
    //{
    //    foreach (var plane in planeManager.trackables)
    //    {
    //        plane.boundaryChanged += boundaryChanged;
    //    }
    //}

    //private void OnDisable()
    //{
    //    if (standPlane == null)
    //    {
    //        unsubcribeBoundaryChangedEvent();
    //    }
    //}

    //void unsubcribeBoundaryChangedEvent()
    //{
    //    foreach (var plane in planeManager.trackables)
    //    {
    //        plane.boundaryChanged -= boundaryChanged;
    //    }
    //}

    //private void boundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
    //{
    //    if(calculatePlaneArea(obj.plane) >= desiredArea)
    //    {

    //        standPlane = obj.plane;
    //        foreach (var plane in planeManager.trackables)
    //        {
    //            plane.gameObject.SetActive(false);
    //        }

    //        standPlane.gameObject.SetActive(true);
    //        planeManager.planesChanged += disableNewPlanes;
    //        unsubcribeBoundaryChangedEvent();
    //    }
    //}

    float calculatePlaneArea(ARPlane plane)
    {
        return plane.size.x * plane.size.y;
    }

    void disableNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false);
        }

        foreach (var plane in args.updated)
        {

        }
    }
}
