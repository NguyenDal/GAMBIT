using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/*
 * Class for the move forward button, captures button press (& being held) & release for movement.
 */
public class MoveForwardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Reference to the Participant GameObject
    public GameObject participant;

    // Speed of movement
    public float moveSpeed = 5f;

    // Flags to track button press state
    private bool isMoving = false;

    // Start moving the participant forward when the button is pressed
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

    // Coroutine to continuously move the participant forward while the button is held down
    private IEnumerator MoveParticipantCoroutine()
    {
        while (isMoving)
        {
            // Move the participant forward
            participant.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }
    }
}
