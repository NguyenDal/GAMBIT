using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class input : MonoBehaviour
{

    public UnityEvent unityEvent;



    public UnityEvent otherEvent;

 

        // Update is called once per frame
    void Update()
    {
        // pretend theres an if statement here that checks if player mouse is in a specific corner of screen.

        if (Input.GetButton("Fire1"))
        {
            unityEvent.Invoke();
        }

        if (Input.GetButton("Fire2"))
        {
            otherEvent.Invoke();
        }
    }
}
