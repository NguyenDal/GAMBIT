using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MoveRightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Reference to the Participant GameObject
    public GameObject participant;

    // Reference to the Camera GameObject
    public GameObject cameraObject;

    // Speed of rotation
    public float rotationSpeed = 50f;

    // Flags to track button press state
    private bool isRotating = false;

    // Start rotating the camera right when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        isRotating = true;
    }

    // Stop rotating the camera when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            // Rotate the participant and camera to the right
            participant.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            cameraObject.transform.RotateAround(participant.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
