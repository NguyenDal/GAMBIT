using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateAction", order = 1)]
public class ActionObject : ScriptableObject
{
    public string actionName;
    public string keyName;

    public UnityEvent eventName;

    void checkForInput()
    {
       // if (Input.GetButton)
    }
    
}
