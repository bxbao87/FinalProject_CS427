using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;
    private Dictionary<string, KeyValuePair<GameObject, Quaternion>> spawnedPrefabs = new Dictionary<string, KeyValuePair<GameObject, Quaternion>>();
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    float scaleFactor = 0.05f;
    [SerializeField]
    float btnScaleFactor = 0.1f;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        Vector3 rot = new Vector3(0f, 180f, 0f);
        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab);
            newPrefab.transform.localScale *= scaleFactor;
            createButton(newPrefab);

            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, 
                new KeyValuePair<GameObject, Quaternion>
                (newPrefab, Quaternion.Euler(newPrefab.transform.rotation.eulerAngles + rot)));
        }    
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                UpdateImage(trackedImage);
            }
            else
            {
                spawnedPrefabs[trackedImage.referenceImage.name].Key.SetActive(false);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.referenceImage.name].Key.SetActive(false);
        }
    }
    
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        KeyValuePair<GameObject, Quaternion> pair = spawnedPrefabs[name];
        GameObject prefab = pair.Key;
        prefab.transform.position = position;
        prefab.transform.rotation = trackedImage.transform.rotation * pair.Value;
        prefab.SetActive(true);
        //foreach(GameObject go in spawnedPrefabs.Values)
        //{
        //    if(go.name != name)
        //    {
        //        go.SetActive(false);
        //    }    
        //} 
    }

    private void createButton(GameObject obj)
    {
        var button = Instantiate(buttonPrefab, obj.transform, true);
        button.transform.position += new Vector3(0.1f, 0, 0);
        //button.transform.rotation = obj.transform.rotation;
        button.transform.localScale *= btnScaleFactor;
    }

}
