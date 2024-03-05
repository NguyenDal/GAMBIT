//version 1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadController : MonoBehaviour
{
    Vector3 move;
    public GameObject player;
    public float speed = 1f;

    void Start()
    {
        for(int i = 0; i < Gamepad.all.Count; i++) {
            Debug.Log(Gamepad.all[i].name);
        }
    }

    void Update()
    {
        move = Vector3.zero;

        if (Gamepad.all.Count > 0) {
            if (Gamepad.all[0].buttonWest.isPressed) {
                MoveLeft();
            }
            if (Gamepad.all[0].buttonEast.isPressed) {
                MoveRight();
            }
            if (Gamepad.all[0].buttonNorth.isPressed) {
                MoveForward();
            }
            if (Gamepad.all[0].buttonSouth.isPressed) {
                MoveBackward();
            }
        }
    }

    void MoveLeft()
    {
        player.transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void MoveRight()
    {
        player.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    void MoveForward()
    {
        player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void MoveBackward()
    {
        player.transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
}