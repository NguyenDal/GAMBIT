using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;
    public AudioSource audioSource;
    public Slider musicSlider;

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load and apply the saved volume level from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        ApplySavedVolume(savedVolume);

        if (musicSlider != null)
        {
            // Add listener to handle slider value changes
            musicSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void ApplySavedVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }

        if (musicSlider != null)
        {
            musicSlider.value = volume;
        }

        Debug.Log("Applied saved volume: " + volume);
    }

    public void SetVolume(float volume)
    {
        // Set the volume on the AudioSource
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }

        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}