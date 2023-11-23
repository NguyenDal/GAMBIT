using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class MovementController : MonoBehaviour
{
    public UnityEvent unityEvent;
    public UnityEvent otherEvent;

    public float moveSpeed = 5f;  // can change speed based on requirements

    // Method to calculate x and y movement values
    public Vector2 CalculateMovement(float inputX, float inputY)
    {
        // Calculate movement values based on input
        float moveX = inputX * moveSpeed * Time.deltaTime;
        float moveY = inputY * moveSpeed * Time.deltaTime;

        return new Vector2(moveX, moveY);
    }

    //example of Update() method
    void Update()
    {
        // Example input (you can replace this with your actual input)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement using the CalculateMovement method
        Vector2 movement = CalculateMovement(horizontalInput, verticalInput);

        // Apply the movement to the object's position
        //transform.Translate(movement);
    }
}
