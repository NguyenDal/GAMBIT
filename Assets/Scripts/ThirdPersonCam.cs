using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public bool isCursorLocked = true;

    private void Start()
    {
       
  
    }

    private void Update()
    {

        if (player == null)
        {
            Debug.LogWarning("Player is not assigned.");
            return;
        }

        // Set the camera's position to match the player's position
        transform.position = player.position;

        // Update the camera's rotation to look at the player's position
        transform.LookAt(player);


        Cursor.lockState = CursorLockMode.Locked;

    }

}