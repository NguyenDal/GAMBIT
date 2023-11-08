using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "InputObject", order = 1)]
public class InputObject : ScriptableObject
{
    // this should be set during 'calibration'
    public int hzValue;

    // this is set for testing / debug purposes. 
    public string keyName;

    public enum checkType
    {
        pressed,
        held,
        released
    }

    public checkType currentCheckType = checkType.pressed;

    public UnityEvent unityEvent;
}
