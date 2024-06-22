using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Slider brightnessSlider;  
    public Light DirectionalLight;         

    public void Start()
    {
        // Retrieve the saved brightness value, defaulting to 1 if not found
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        brightnessSlider.value = savedBrightness;
        DirectionalLight.intensity = Mathf.PingPong(Time.time, savedBrightness);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChange);
    }

    void OnBrightnessChange(float value)
    {
        DirectionalLight.intensity = value;
        PlayerPrefs.SetFloat("Brightness", value);  // Save the current brightness value
    }
}
