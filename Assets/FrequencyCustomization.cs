using UnityEngine;
using UnityEngine.UI;

public class FrequencyCustomization : MonoBehaviour
{
    // References to ButtonFlicker instances (for UI flickering)
    private ButtonFlicker upButtonFlicker;
    private ButtonFlicker downButtonFlicker;
    private ButtonFlicker leftButtonFlicker;
    private ButtonFlicker rightButtonFlicker;

    // Reference to the single UI Slider
    public Slider FrequencySlider;

    void Start()
    {
        // Find the buttons programmatically by name
        upButtonFlicker = GameObject.Find("Forward-Button").GetComponent<ButtonFlicker>();
        downButtonFlicker = GameObject.Find("Backward-Button").GetComponent<ButtonFlicker>();
        leftButtonFlicker = GameObject.Find("Left-Button").GetComponent<ButtonFlicker>();
        rightButtonFlicker = GameObject.Find("Right-Button").GetComponent<ButtonFlicker>();

        // Initialize the slider with the current frequency (e.g., use the Up button's frequency)
        if (upButtonFlicker != null)
            FrequencySlider.value = upButtonFlicker.frequency;

        // Add a listener to the slider
        FrequencySlider.onValueChanged.AddListener(UpdateAllFrequencies);
    }

    void UpdateAllFrequencies(float value)
    {
        // Update all ButtonFlicker frequencies
        if (upButtonFlicker != null) upButtonFlicker.UpdateFrequency(value);
        if (downButtonFlicker != null) downButtonFlicker.UpdateFrequency(value);
        if (leftButtonFlicker != null) leftButtonFlicker.UpdateFrequency(value);
        if (rightButtonFlicker != null) rightButtonFlicker.UpdateFrequency(value);
    }
}