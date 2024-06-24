using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Slider brightnessSlider;  
    public Light DirectionalLight;         

    public void Start()
    {
        float defaultBrightness = 2f;

        // Save the default brightness value to PlayerPrefs if it hasn't been set before
        if (!PlayerPrefs.HasKey("Brightness"))
        {
            PlayerPrefs.SetFloat("Brightness", defaultBrightness);
        }

        float savedBrightness = PlayerPrefs.GetFloat("Brightness", defaultBrightness);
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
