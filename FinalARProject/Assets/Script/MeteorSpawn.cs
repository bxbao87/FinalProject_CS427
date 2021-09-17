using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public PlaneStopTracking planeStopTracking;

    [SerializeField]
    int t1=5, t2=10;


    void Start()
    {
        StartCoroutine(meteorWave());
    }

    void spawnMeteor()
    {
        var standPlane = planeStopTracking.standPlane;
        if (standPlane != null)
        {
            var meteor = Instantiate(meteorPrefab);
            meteor.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            Vector3 tmp = FoodSpawn.randomPosition(standPlane);
            tmp.y += 20;
            meteor.transform.position = tmp;
            meteor.GetComponent<MeteorLanding>().landHeight = standPlane.transform.position.y;
        }
    }

    IEnumerator meteorWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(t1, t2));
            spawnMeteor();
        }
    }

}
