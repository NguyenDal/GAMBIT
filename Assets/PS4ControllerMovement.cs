using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PS4ControllerMovement : MonoBehaviour
{
    public TextMeshProUGUI text;
    private int hertz;
    private int frame;
    public GameObject player;
    public float movementSpeed = 0.5f; // Adjust this value as needed

    // Start is called before the first frame update
    void Start()
    {
        hertz = 0;
        frame = 0;

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        frame++;

        // Getting hertz value based on left stick input every 30 frames
        if (frame == 30)
        {
            frame = 0;

            if (Gamepad.all.Count > 0)
            {
                if (Gamepad.all[0].leftStick.up.isPressed & hertz <= 39)
                {
                    hertz++;
                }
                else if (Gamepad.all[0].leftStick.down.isPressed & hertz >= 1)
                {
                    hertz--;
                }
            }
        }

        // Displaying the new hertz value
        text.SetText(hertz.ToString() + " hertz");

        // Calculate movement direction based on hertz value
        Vector3 movementDirection = Vector3.zero;
        switch (hertz)
        {
            case 10:
                movementDirection = Vector3.forward;
                break;
            case 20:
                movementDirection = Vector3.right;
                break;
            case 30:
                movementDirection = Vector3.back;
                break;
            case 40:
                movementDirection = Vector3.left;
                break;
            default:
                break;
        }

        // Update player's position based on movement direction
        if (movementDirection != Vector3.zero)
        {
            Vector3 movement = transform.TransformDirection(movementDirection) * movementSpeed * Time.deltaTime;
            player.transform.position += movement;
        }
    }

}