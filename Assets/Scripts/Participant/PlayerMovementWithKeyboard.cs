using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementWithKeyboard : MonoBehaviour
{
    // Default movement and rotation speeds (will be overwritten by PlayerPrefs)
    public float defaultMovementSpeed = 4f;
    public float defaultRotationSpeed = 100f;
    private Vector3 movementDirection = Vector3.zero;
    private bool stopMovement = false;

    void Start()
    {
        // Disable keyboard player movement if frequency movement is enabled
        if (gameObject.GetComponent<FrequencyMovement>() != null && gameObject.GetComponent<FrequencyMovement>().frequencyMovementEnabled)
        {
            this.enabled = false;
        }

        // Retrieve movement speed from PlayerPrefs, default to defaultMovementSpeed if not found
        float movementSpeed = PlayerPrefs.GetFloat("MovementSpeed", defaultMovementSpeed);
        // Retrieve rotation speed from PlayerPrefs, default to defaultRotationSpeed if not found
        float rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed", defaultRotationSpeed);

        if (movementSpeed > 4)
            movementSpeed = 4f;
        if (rotationSpeed > 100)
            rotationSpeed = 100f;

        // Apply retrieved speeds
        SetMovementSpeed(movementSpeed);
        SetRotationSpeed(rotationSpeed);
    }

    void Update()
    {
        if (stopMovement)
        {
            // Stop all movement
            return;
        }

        // Allows interaction using regular keyboard movement
        if (Keyboard.current.pKey.isPressed)
        {
            gameObject.GetComponent<InteractionHandler>().BreakWall();
        }

        // Calculate movement direction based on W and S key input
        movementDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
            movementDirection += transform.forward;
        if (Keyboard.current.sKey.isPressed)
            movementDirection -= transform.forward;

        // Normalize movement direction to prevent diagonal movement being faster
        movementDirection.Normalize();

        // Update player's position based on movement direction and speed
        Vector3 movement = movementDirection * GetMovementSpeed() * Time.deltaTime;
        transform.position += movement;

        // Calculate rotation based on A and D key input
        float rotationInput = 0f;
        if (Keyboard.current.aKey.isPressed)
            rotationInput -= 1f;
        if (Keyboard.current.dKey.isPressed)
            rotationInput += 1f;

        // Rotate player based on horizontal input
        transform.Rotate(Vector3.up, rotationInput * GetRotationSpeed() * Time.deltaTime);
    }

    // Method to set movement speed
    private void SetMovementSpeed(float speed)
    {
        PlayerPrefs.SetFloat("MovementSpeed", speed);
        PlayerPrefs.Save();
    }

    // Method to get movement speed
    private float GetMovementSpeed()
    {
        return PlayerPrefs.GetFloat("MovementSpeed", defaultMovementSpeed);
    }

    // Method to set rotation speed
    private void SetRotationSpeed(float speed)
    {
        PlayerPrefs.SetFloat("RotationSpeed", speed);
        PlayerPrefs.Save();
    }

    // Method to get rotation speed
    private float GetRotationSpeed()
    {
        return PlayerPrefs.GetFloat("RotationSpeed", defaultRotationSpeed);
    }

    // Method to stop movement
    public void StopMovement()
    {
        stopMovement = true;
        movementDirection = Vector3.zero;
    }

    // Method to reset movement
    public void ResetMovement()
    {
        stopMovement = false;
    }
}
