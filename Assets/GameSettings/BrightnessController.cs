using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Slider brightnessSlider;
    public Light directionalLight;

    void Start()
    {
        float defaultBrightness = 2.5f;
        if (!PlayerPrefs.HasKey("Brightness"))
        {
            PlayerPrefs.SetFloat("Brightness", defaultBrightness);
            brightnessSlider.value = defaultBrightness;
        }
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", defaultBrightness);

        brightnessSlider.value = savedBrightness;
        UpdateLightBrightness(savedBrightness);

        brightnessSlider.onValueChanged.AddListener(OnBrightnessChange);
    }

    void OnBrightnessChange(float value)
    {
        UpdateLightBrightness(value);
        PlayerPrefs.SetFloat("Brightness", value);
    }

    void UpdateLightBrightness(float brightness)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = Mathf.PingPong(Time.time, brightness); ;
        }
    }
}
