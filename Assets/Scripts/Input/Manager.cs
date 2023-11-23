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

    public int pressWaitTime = 50;

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
                if (Input.GetButton(inputList[i].keyName))
                {
                    getMovmentInputs(inputList[i].keyName);
                    sendInput.Invoke(rotationInput, movementInput, "w");
                }
            }
        }
    }

    public void getMovmentInputs(string currentButton) 
    {
        if (currentButton.Equals("w"))
        {
            movementInput = (float)0.3;
            rotationInput = 0;
        }
        else if (currentButton.Equals("a"))
        {
            movementInput = 0;
            rotationInput = (float)-0.3;
        }
        //location is bottom right
        else if (currentButton.Equals("s"))
        {
            movementInput = (float)-0.3;
            rotationInput = 0;
        }
        //location is bottom left
        else if (currentButton.Equals("d"))
        {
            movementInput = 0;
            rotationInput = (float)0.3;
        }
    }


    public inputStage getInputStage()
    {
        bool keyDown = false;
        inputStage result = inputStage.released;
        for (int i = 0; i < inputList.Count; i++)
        {
            //if a key is down check if being held or pressed;
            if (Input.GetButton(inputList[i].keyName))
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
