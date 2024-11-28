using System;
using System.Collections;
using System.Collections.Generic;
using OpenSee;
using UnityEngine;
using static OpenSee.OpenSee;

public class TrackingAPI : MonoBehaviour
{
    private OpenSee.OpenSee openSeeComponent;


    private Dictionary<Func<OpenSeeData, bool>, Action<OpenSeeData>> bindableList = new Dictionary<Func<OpenSeeData, bool>, Action<OpenSeeData>>();


    public void bindToList(Func<OpenSeeData, bool> checker, Action<OpenSeeData> action)
    {
        bindableList.Add(checker, action);
    }

    // Start is called before the first frame update
    void Start()
    {
        openSeeComponent = GetComponent<OpenSee.OpenSee>();

    }



    void testAction(OpenSeeData data)
    {
        Debug.Log("left eye is closed!!!!");
    }

    // Update is called once per frame
    void Update()
    {
        OpenSeeData data = openSeeComponent.GetOpenSeeData(0);
        if (data != null)
        {
            foreach (KeyValuePair<Func<OpenSeeData, bool>, Action<OpenSeeData>> keyValuePair in bindableList)
            {
                Func<OpenSeeData, bool> checkerFunc = keyValuePair.Key;
                Action<OpenSeeData> action = keyValuePair.Value;
                bool doAction = checkerFunc.Invoke(data);
                if (doAction)
                {
                    action.Invoke(data);
                }

            }
        }
    }
}
