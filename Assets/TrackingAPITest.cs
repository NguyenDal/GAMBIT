using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OpenSee.OpenSee;

public class TrackingAPITest : MonoBehaviour
{
    // Start is called before the first frame update
    TrackingAPI TrackingAPI;
    GameObject openSee;

    void Start()
    {
        openSee = GameObject.Find("OpenSee");

        TrackingAPI = openSee.GetComponent<TrackingAPI>();

        TrackingAPI.bindToList(eyeCheckerTest, eyeActionTest);
    }


    public bool eyeCheckerTest(OpenSeeData data)
    {
        if (data.leftEyeOpen < 0.5)
        {
            return true;
        }

        return false;
    }


    public void eyeActionTest(OpenSeeData openSeeData)
    {
        Debug.Log("Left eye closed!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
