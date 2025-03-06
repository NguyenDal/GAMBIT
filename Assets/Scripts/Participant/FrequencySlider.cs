using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrequencySlider : MonoBehaviour
{
    public Slider slider; // Reference to the slider
    public TextMeshProUGUI frequencyText; // Reference to the UI text to display the frequency
    private const string frequencyFormat = "Frequency: {0:0.0} Hz"; // Format for the text

    void Start()
    {
        // Ensure the slider starts with the correct value
        if (slider != null && frequencyText != null)
        {
            slider.onValueChanged.AddListener(UpdateFrequencyDisplay);
            UpdateFrequencyDisplay(slider.value); // Set initial value
        }
    }

    // Method to update text when slider value changes
    void UpdateFrequencyDisplay(float value)
    {
        if (frequencyText != null)
        {
            frequencyText.text = string.Format(frequencyFormat, value);
        }
    }
}
