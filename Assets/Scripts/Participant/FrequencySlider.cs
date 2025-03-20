using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrequencySlider : MonoBehaviour
{
    public Slider slider; 
    public TextMeshProUGUI frequencyText; // Shows the frequency in the settings

    private const string frequencyFormat = "{0:0.0} Hz";

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(UpdateFrequency);
            UpdateFrequency(slider.value); // Set initial value
        }
    }

    void UpdateFrequency(float value)
    {
        FrequencyManager.Instance.SetFrequency(value); // Store the value globally

        if (frequencyText != null) 
        {
            frequencyText.text = string.Format(frequencyFormat, value);
        }
    }
}
