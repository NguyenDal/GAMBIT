using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/*
 * Class for the move back button, captures button press (& being held) & release for movement.
 */
public class MoveBackwardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Reference to the Participant GameObject
    public GameObject participant;

    // Speed of movement
    public float moveSpeed = 4f;

    // Flags to track button press state
    private bool isMoving = false;

    void Start()
    {
        GameSettings.LoadSettings();

        // Retrieve movement speed from PlayerPrefs, default to defaultMovementSpeed if not found
        float movementSpeed = PlayerPrefs.GetFloat("MovementSpeed", moveSpeed);

        if (movementSpeed > 4)
            movementSpeed = 4f;

        // Apply retrieved speeds
        SetMovementSpeed(movementSpeed);
    }

    // Start moving the participant backward when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        isMoving = true;
        StartCoroutine(MoveParticipantCoroutine());
    }

    // Stop moving the participant when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isMoving = false;
    }

    // Coroutine to continuously move the participant backward while the button is held down
    private IEnumerator MoveParticipantCoroutine()
    {
        Vector3 movementDirection = Vector3.zero;
        movementDirection -= transform.forward;
 
       
        while (isMoving)
        {
            // Move the participant backward
            participant.transform.Translate(movementDirection * GetMovementSpeed() * Time.deltaTime);
 
            // Wait for the next frame
            yield return null;
        }
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
        return PlayerPrefs.GetFloat("MovementSpeed", moveSpeed);
    }
}
