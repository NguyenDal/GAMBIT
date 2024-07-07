using UnityEngine;
using UnityEngine.UI;

public class ColorBlindEffectManager : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera
    public Button protanopiaButton;
    public Button deuteranopiaButton;
    public Button tritanopiaButton;
    public Button normalVisionButton;

    private CameraColorBlindEffect cameraColorBlindEffect;

    void Start()
    {
        if (mainCamera != null)
        {
            cameraColorBlindEffect = mainCamera.GetComponent<CameraColorBlindEffect>();
            if (cameraColorBlindEffect == null)
            {
                cameraColorBlindEffect = mainCamera.gameObject.AddComponent<CameraColorBlindEffect>();
            }
        }

        if (protanopiaButton != null)
        {
            protanopiaButton.onClick.AddListener(() => SetColorBlindMode(CameraColorBlindEffect.ColorBlindMode.Protanopia));
        }
        if (deuteranopiaButton != null)
        {
            deuteranopiaButton.onClick.AddListener(() => SetColorBlindMode(CameraColorBlindEffect.ColorBlindMode.Deuteranopia));
        }
        if (tritanopiaButton != null)
        {
            tritanopiaButton.onClick.AddListener(() => SetColorBlindMode(CameraColorBlindEffect.ColorBlindMode.Tritanopia));
        }
        if (normalVisionButton != null)
        {
            normalVisionButton.onClick.AddListener(() => SetColorBlindMode(CameraColorBlindEffect.ColorBlindMode.Normal));
        }
    }

    void SetColorBlindMode(CameraColorBlindEffect.ColorBlindMode mode)
    {
        if (cameraColorBlindEffect != null)
        {
            cameraColorBlindEffect.colorblindMode = mode;
        }
    }
}
