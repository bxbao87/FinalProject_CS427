using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    float timeFactor = 15f;

    public ARRaycastManager raycastManager;

    public ARPlane standPlane;

    Animator anim;
    bool isWalking = false;
    bool isEating = false;
    private TimerCountDown timerCountDown;

    private Vector3 appliedRot;
    void Start()
    {
        //raycastManager = GetComponent<ARRaycastManager>();
        anim = GetComponent<Animator>();
        timerCountDown = GameObject.Find("TimerCountDown").GetComponent<TimerCountDown>();
        appliedRot = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (isEating)
            return;

        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var hitResults = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hitResults, TrackableType.PlaneWithinBounds);

        
        if (hitResults.Count > 0)
        {
            ARRaycastHit hit = hitResults.Find(x => x.trackableId == standPlane.trackableId);
            if (hit != null)
            {
                Vector3 followedPos = hit.pose.position;
                Vector3 curPos = transform.position;
                if (Vector3.Distance(followedPos, curPos) > 0.1)
                {
                    Quaternion lookRot = Quaternion.LookRotation((followedPos - curPos));
                    lookRot *= Quaternion.Euler(appliedRot);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * timeFactor);
                    transform.position = Vector3.MoveTowards(curPos, followedPos, Time.deltaTime * speed);
                    isWalking = true;
                    return;
                }
            }
        }

        isWalking = false;
    }

    private void FixedUpdate()
    {
        anim.SetBool("walking", isWalking);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        if (otherObj.CompareTag("food"))
        {
            isEating = true;
            isWalking = false;
            anim.SetTrigger("eating");
            StartCoroutine(Eating(otherObj));
        }
        else if (otherObj.CompareTag("die"))
        {
            EndGame();
        }
    }

    IEnumerator Eating(GameObject otherObj = null)
    {
        if (otherObj != null)
        {
            float delay = anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(delay);

            otherObj.SetActive(false);
            isEating = false;
        }
    }

    private void EndGame()
    {
        timerCountDown.gameOver();
    }
}
