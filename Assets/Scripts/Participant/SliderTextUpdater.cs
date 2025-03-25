using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextUpdater : MonoBehaviour
{
    public Slider slider;          // Assign your slider in Inspector
    public TextMeshProUGUI text;   // Assign your TMP text in Inspector

    void Start()
    {
        // Update text when slider changes
        slider.onValueChanged.AddListener(UpdateText);
        UpdateText(slider.value);  // Initialize text
    }

    void UpdateText(float value)
    {
        text.text = string.Format("{0:0.0} Hz", value);
    }
}