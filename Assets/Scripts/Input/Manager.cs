using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class sendKeyBoardInput : UnityEvent<float, float, string> { }

public class Manager : MonoBehaviour
{
    public List<InputObject> inputList;

    public int pressCounter = 0;
     
    public int pressWaitTime = 50; //Amount of physics updates that need to pass before button is held

    public float movementInput = 0;

    public float rotationInput = 0;

    public sendKeyBoardInput sendInput;

    public enum inputStage
    {
        pressed,
        held,
        released
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            // todo; function for however the HZ value is being checked for (driver team, this is up to you i guess). 
            if (inputList[i].keyName != null)
            {
                print(getInputStage().ToString());
                if (Input.GetKey(inputList[i].keyName))
                {
                    sendInput.Invoke(inputList[i].rotationInput, inputList[i].movmentInput, getInputStage().ToString());
                }
            }
        }
    }

    public inputStage getInputStage()
    {
        bool keyDown = false;
        inputStage result = inputStage.released;
        for (int i = 0; i < inputList.Count; i++)
        {
            //if a key is down check if being held or pressed;
            if (Input.GetKey(inputList[i].keyName))
            {
                keyDown = true;
                if(pressCounter > pressWaitTime)
                {
                    result = inputStage.held;
                    break;
                }
                pressCounter++;
                result = inputStage.pressed;
                break;
            }
        }
        //no key down, reset counter
        if (!keyDown)
        {
            pressCounter = 0;
        }
        return result;
            
    }
}
