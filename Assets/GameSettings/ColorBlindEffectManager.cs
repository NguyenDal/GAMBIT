using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorBlindEffectManager : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera
    public Button protanopiaButton;
    public Button deuteranopiaButton;
    public Button tritanopiaButton;
    public Button normalVisionButton;

    private CameraColorBlindEffect cameraColorBlindEffect;
    private const string ColorBlindModeKey = "ColorBlindMode";

    void Awake()
    {
        // Load the saved colorblind mode from PlayerPrefs
        int savedMode = PlayerPrefs.GetInt(ColorBlindModeKey, (int)CameraColorBlindEffect.ColorBlindMode.Normal);

        if (mainCamera != null)
        {
            cameraColorBlindEffect = mainCamera.GetComponent<CameraColorBlindEffect>();
            if (cameraColorBlindEffect == null)
            {
                cameraColorBlindEffect = mainCamera.gameObject.AddComponent<CameraColorBlindEffect>();
            }
            cameraColorBlindEffect.colorblindMode = (CameraColorBlindEffect.ColorBlindMode)savedMode;
        }

        // Set up button listeners
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

        // Ensure the component persists across scenes
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void SetColorBlindMode(CameraColorBlindEffect.ColorBlindMode mode)
    {
        if (cameraColorBlindEffect != null)
        {
            cameraColorBlindEffect.colorblindMode = mode;
            PlayerPrefs.SetInt(ColorBlindModeKey, (int)mode);
            PlayerPrefs.Save(); // Ensure that the PlayerPrefs is saved immediately
            Debug.Log("Saved ColorBlindMode: " + (int)mode); // Debug log
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            cameraColorBlindEffect = mainCamera.GetComponent<CameraColorBlindEffect>();
            if (cameraColorBlindEffect == null)
            {
                cameraColorBlindEffect = mainCamera.gameObject.AddComponent<CameraColorBlindEffect>();
            }

            // Apply the saved colorblind mode
            int savedMode = PlayerPrefs.GetInt(ColorBlindModeKey, (int)CameraColorBlindEffect.ColorBlindMode.Normal);
            cameraColorBlindEffect.colorblindMode = (CameraColorBlindEffect.ColorBlindMode)savedMode;
            Debug.Log("Loaded ColorBlindMode: " + savedMode); // Debug log
        }
    }
}
