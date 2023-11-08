using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    public List<InputObject> inputList;


    public UnityEvent unityEvent;

    public enum checkType
    {
        pressed,
        held,
        released
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inputList.Count; i++)
        {       
            // todo; function for however the HZ value is being checked for (driver team, this is up to you i guess). 
            if (Input.GetButton(inputList[i].keyName))
            {
                //inputList[i].testFunction();
                //inputList[i].unityEvent.Invoke();
            }
        }
    }
}
