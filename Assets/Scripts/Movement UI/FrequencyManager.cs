using UnityEngine;

public class FrequencyManager : MonoBehaviour
{
    public static FrequencyManager Instance; // Singleton instance
    public float ButtonFrequency { get; private set; } // Frequency value

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to update the frequency (called by the slider)
    public void SetFrequency(float frequency)
    {
        ButtonFrequency = frequency;
    }
}
