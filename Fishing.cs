using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Fishing : MonoBehaviour
{
    public OnLineThrow throwingAnimation;

    public GameObject bobber;
    public static GameObject currentBobber;

    public Transform lineTip;

    public Transform player;
    Vector3 direction;

    public LineRenderer line;

    public float throwSpeed = 50f;
    bool threwLine;
    public static bool attemptedCatch = false;
    float distanceToBreak = 30f;

    public LayerMask water;
    public LayerMask inventoryGraphic;

    bool animationIsPlaying = false;

    public Coroutine routine;

    // Start is called before the first frame update
    void Start()
    {
        currentBobber = null;

        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        direction = (player.forward + new Vector3(0, .3f, 0)) * throwSpeed;

        if(currentBobber != null)
        {
            line.enabled = true;
        } else
        {
            line.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, Mathf.Infinity, inventoryGraphic))
                OnMouseClick();
        }

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, Mathf.Infinity, inventoryGraphic))
                OnMouseClickRight();
        }

        if(currentBobber != null)
        {
            CurveFishingLine();
            if (Vector3.Distance(lineTip.position, currentBobber.GetComponent<Transform>().position) > distanceToBreak)
            {
                OnMouseClickRight(false);
            }
        }
    }

    private void OnMouseClick()
    {
        if(!threwLine && !animationIsPlaying)
        {
            animationIsPlaying = true;
            routine = throwingAnimation.StartCoroutine("ThrowLine");
            
            attemptedCatch = false;
        }
    }
    public void ThrowBobber()
    {
        if(!animationIsPlaying)
        {
            currentBobber = Instantiate(bobber, lineTip.position + (player.forward + new Vector3(0, 0, 0)), Quaternion.identity);
            currentBobber.GetComponent<Rigidbody>().AddForce(direction);

            threwLine = true;
        }
    }

    private void OnMouseClickRight(bool onPurpose = true)
    {
        if(routine != null)
            throwingAnimation.StopCoroutine(routine);

        animationIsPlaying = true;

        float r = UnityEngine.Random.Range(100f, 150f);

        throwingAnimation.StartCoroutine("ReelLine");

        Destroy(currentBobber);
        currentBobber = null;

        threwLine = false;

        if(onPurpose)
            attemptedCatch = true;
    }

    private void CurveFishingLine()
    {
        var pointList = new List<Vector3>();
        Vector3 inBetween = Vector3.Lerp(lineTip.position, currentBobber.GetComponent<Transform>().position, .5f);
        float yDifference = (lineTip.position.y - currentBobber.GetComponent<Transform>().position.y) / 50f;
        float droop = Vector3.Distance(lineTip.position, currentBobber.GetComponent<Transform>().position) * yDifference;

        Vector3 pointA = lineTip.position;
        Vector3 pointB = inBetween - new Vector3(0, droop, 0);
        Vector3 pointC = currentBobber.GetComponent<Transform>().position;

        for (int i = 0; i < 12; i++)
        {
            float ratio = i / 12f;
            Vector3 p1 = Vector3.Lerp(pointA, pointB, ratio);
            Vector3 p2 = Vector3.Lerp(pointB, pointC, ratio);
            pointList.Add(Vector3.Lerp(p1, p2, ratio));
        }

        line.SetPositions(pointList.ToArray());
        
    }

    public static void SetAttemptedCatch(bool state)
    {
        attemptedCatch = state;
    }

    public void SetAnimationIsPlaying(bool state)
    {
        animationIsPlaying = state;
    }

}
