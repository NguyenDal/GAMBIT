using UnityEngine;
using UnityEngine.UI;

public class ChangeImageColour : MonoBehaviour
{
    private Image childImage;

    // Reference to ArduinoButton to access the input flag
    private ArduinoButton arduinoButton ;

    private Color greyColor = Color.grey;
    private Color redColor = Color.red;

    void Start()
    {
        // Assumes the image is the first child
        childImage = GetComponentInChildren<Image>();

        if (childImage == null)
        {
            Debug.LogError("No child Image component found!");
        }

        // Find ArduinoButton in the scene
        arduinoButton = FindObjectOfType<ArduinoButton>();

        if (arduinoButton == null)
        {
            Debug.LogError("ArduinoButton not found in scene!");
        }
    }

    void Update()
    {
        if (childImage != null && arduinoButton != null)
        {
            if (arduinoButton.input)
            {
                childImage.color = redColor;
            }
            else
            {
                childImage.color = greyColor;
            }
        }
    }
}
