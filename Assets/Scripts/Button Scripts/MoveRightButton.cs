using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/*
 * Class for the move right button, captures button press (& being held) & release for movement.
 */
public class MoveRightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Reference to the Participant GameObject
    public GameObject participant;

    // Speed of rotation
    public float rotationSpeed = 180f;

    // Flags to track button press state
    private bool isRotating = false;

    void Start()
    {
        GameSettings.LoadSettings();

        // Retrieve rotation speed from PlayerPrefs, default to defaultRotationSpeed if not found
        float rotateSpeed = PlayerPrefs.GetFloat("RotationSpeed", rotationSpeed);

        // Apply retrieved speeds
        SetRotationSpeed(rotationSpeed);
    }

    // Start rotating right when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        isRotating = true;
    }

    // Stop rotating when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = GameSettings.rotationSpeed;
        if (isRotating)
        {
            // Rotate the participant to the right
            participant.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
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
        return PlayerPrefs.GetFloat("RotationSpeed", rotationSpeed);
    }
}
