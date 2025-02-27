using UnityEngine;
using UnityEngine.UI;

public class FrequencyCustomization : MonoBehaviour
{
    // References to ButtonFlicker instances (for UI flickering)
    private ButtonFlicker upButtonFlicker;
    private ButtonFlicker downButtonFlicker;
    private ButtonFlicker leftButtonFlicker;
    private ButtonFlicker rightButtonFlicker;

    // Separate Sliders for each button
    public Slider ForwardSlider;
    public Slider BackwardSlider;
    public Slider LeftSlider;
    public Slider RightSlider;

    void Start()
    {
        // Find the buttons by name
        upButtonFlicker = GameObject.Find("Forward-Button").GetComponent<ButtonFlicker>();
        downButtonFlicker = GameObject.Find("Backward-Button").GetComponent<ButtonFlicker>();
        leftButtonFlicker = GameObject.Find("Left-Button").GetComponent<ButtonFlicker>();
        rightButtonFlicker = GameObject.Find("Right-Button").GetComponent<ButtonFlicker>();

        // Initialize sliders with the current button frequencies
        if (upButtonFlicker != null) ForwardSlider.value = upButtonFlicker.frequency;
        if (downButtonFlicker != null) BackwardSlider.value = downButtonFlicker.frequency;
        if (leftButtonFlicker != null) LeftSlider.value = leftButtonFlicker.frequency;
        if (rightButtonFlicker != null) RightSlider.value = rightButtonFlicker.frequency;

        // Add listeners for each slider
        ForwardSlider.onValueChanged.AddListener(UpdateForwardFrequency);
        BackwardSlider.onValueChanged.AddListener(UpdateBackwardFrequency);
        LeftSlider.onValueChanged.AddListener(UpdateLeftFrequency);
        RightSlider.onValueChanged.AddListener(UpdateRightFrequency);
    }

    void UpdateForwardFrequency(float value)
    {
        if (upButtonFlicker != null) upButtonFlicker.UpdateFrequency(value);
    }

    void UpdateBackwardFrequency(float value)
    {
        if (downButtonFlicker != null) downButtonFlicker.UpdateFrequency(value);
    }

    void UpdateLeftFrequency(float value)
    {
        if (leftButtonFlicker != null) leftButtonFlicker.UpdateFrequency(value);
    }

    void UpdateRightFrequency(float value)
    {
        if (rightButtonFlicker != null) rightButtonFlicker.UpdateFrequency(value);
    }
}