using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class sendInputClass : UnityEvent<float, float, string> { }
public enum screenPositions
{
    topRight,
    topLeft,
    bottomRight,
    bottomLeft
}

public class InputHandler : MonoBehaviour
{
    public sendInputClass sendInput;
    public float rotationInput = 0;
    public float movementInput = 0;
    public string keyToClick;
    public MockObject mock = new MockObject();
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //getting mouse position from mock object 
        mock.mouseQuadrent();
        screenPositions currentPos = mock.getCurrentMousePosition();

        //get movement values from mouse position 
        eyeTrackPositionToInputValue(currentPos);

        keyToClick = calculateKeyToClick(mock.getHzFrequency());

        //send values to PlayerController
        sendInput.Invoke(rotationInput, movementInput, keyToClick);
    }

    void eyeTrackPositionToInputValue(screenPositions currentScreenPositions)
    {
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
    public string calculateKeyToClick(int hzValue)
    {
        //low HZ value
        if (hzValue == 1 || hzValue == 2 || hzValue == 3)
        {
            return "Q";
        }
        //mid HZ value
        else if (hzValue == 4 || hzValue == 5 || hzValue == 6)
        {
            return "W";
        }
        //high HZ value
        else if (hzValue == 7 || hzValue == 8 || hzValue == 9)
        {
            return "E";
        }
        //unexpected HZ value 
        else
        {
            return null;
        }
    }

}

public class MockObject
{
    public Vector3 mousePosition; 
    public screenPositions currentMousePosition  = new screenPositions();

    public void mouseQuadrent()
    {
        mousePosition = Input.mousePosition;
        //mouse is bottom left
        if ((mousePosition.x < Screen.width / 2) && (mousePosition.y < Screen.height / 2))
        {
            currentMousePosition = screenPositions.bottomLeft;
        }
        //mouse is bottom right 
        else if ((mousePosition.x > Screen.width / 2) && (mousePosition.y < Screen.height / 2))
        {
            currentMousePosition = screenPositions.bottomRight;
        }
        //mouse is top left 
        else if ((mousePosition.x < Screen.width / 2) && (mousePosition.y > Screen.height / 2))
        {
            currentMousePosition = screenPositions.topLeft;
        }
        //mouse is top right 
        else if ((mousePosition.x > Screen.width / 2) && (mousePosition.y > Screen.height / 2))
        {
            currentMousePosition = screenPositions.topRight;
        }
    }
    public screenPositions getCurrentMousePosition()
    {
        return currentMousePosition;
    }

    public int getHzFrequency()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 9;
        }
        else
        {
            return 0;
        }
    }
}
