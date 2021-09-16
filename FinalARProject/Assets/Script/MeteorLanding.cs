using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorLanding : MonoBehaviour
{
    public float landHeight = 0;

    private void Update()
    {
        if(transform.position.y <= landHeight)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        if (otherObj.CompareTag("land"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
