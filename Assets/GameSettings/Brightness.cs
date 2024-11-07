using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;

    void Start()
    {
        brightness.TryGetSettings(out exposure);

        // Load the saved brightness value from PlayerPrefs
        float savedBrightness = PlayerPrefs.GetFloat("BrightnessValue", 1.0f); // Default to 1.0 if not found

        // Set the brightness using the saved value
        SetBrightness(savedBrightness);

        // Set the slider's value to the saved brightness
        brightnessSlider.value = savedBrightness;

        // Add listener to the slider to adjust brightness
        brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
    }

    public void AdjustBrightness(float value)
    {
        // Set the brightness based on the slider's value
        SetBrightness(value);

        // Save the current brightness value to PlayerPrefs
        PlayerPrefs.SetFloat("BrightnessValue", value);
    }

    private void SetBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.05f;
        }
    }
}
