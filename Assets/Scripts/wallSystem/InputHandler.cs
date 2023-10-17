using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class sendInputClass : UnityEvent<float, float, string> { }

public class InputHandler : MonoBehaviour
{
    public sendInputClass sendInput;
    private float rotationInput = 0;
    private float movementInput = 0;
    public int hzValue;
    public string keyToClick;
    public enum screenPositions
    {
        topRight,
        topLeft,
        bottomRight,
        bottomLeft
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        eyeTrackPositionToInputValue();
        calculateKeyToClick();
        sendInput.Invoke(rotationInput, movementInput, keyToClick);
    }

    void eyeTrackPositionToInputValue()
    {
        //for testing purposes only / remove once mock object is implemted 
        screenPositions currentScreenPositions = screenPositions.topRight;
        //location is top right
        if (currentScreenPositions == screenPositions.topRight)
        {
            movementInput = (float)0.3;
            rotationInput = 0;
        } 
        //location is top left
        else if (currentScreenPositions == screenPositions.topLeft)
        {
            movementInput = (float)0.3;
            rotationInput = 0;
        }
        //location is bottom right
        else if (currentScreenPositions == screenPositions.bottomRight)
        {
            movementInput = 0;
            rotationInput = (float)0.3;
        }
        //location is bottom left
        else if (currentScreenPositions == screenPositions.bottomLeft)
        {
            movementInput = 0;
            rotationInput = (float)-0.3;
        }
    }

    //hhhhhhhhh

    void calculateKeyToClick()
    {
        //low HZ value
        if (hzValue == 1 || hzValue == 2 || hzValue == 3)
        {
            keyToClick = "Q";
        }
        //mid HZ value
        else if (hzValue == 4 || hzValue == 5 || hzValue == 6)
        {
            keyToClick = "W";
        }
        //high HZ value
        else if (hzValue == 7 || hzValue == 8 || hzValue == 9)
        {
            keyToClick = "E";
        }
        //unexpected HZ value 
        else
        {

        }
    }

}
