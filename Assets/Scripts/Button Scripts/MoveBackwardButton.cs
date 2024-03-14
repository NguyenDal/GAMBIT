using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MoveBackwardButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Reference to the Participant GameObject
    public GameObject participant;

    // Speed of movement
    public float moveSpeed = 5f;

    // Flags to track button press state
    private bool isMoving = false;

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
        while (isMoving)
        {
            // Move the participant backward
            participant.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }
    }
}
