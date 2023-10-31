using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateAction", order = 1)]
public class ActionObject : ScriptableObject
{
    public string actionName;
    public string keyName;

    public Action action;
}
