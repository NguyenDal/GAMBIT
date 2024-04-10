using UnityEngine;
using UnityEngine.UI;
using data;
using DS = data.DataSingleton;

public class SpeedsController : MonoBehaviour
{
    public Slider movementSpeedSlider;
    public Slider rotationSpeedSlider;

    private float prevMovementSpeed;
    private float prevRotationSpeed;

    private void Start()
    {
        // Subscribe to the DataLoaded event
        DataSingleton.DataLoaded += InitializeSliders;

        // Set the initial values of sliders based on data or PlayerPrefs
        InitializeSliders();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the DataLoaded event to prevent memory leaks
        DataSingleton.DataLoaded -= InitializeSliders;
    }

    // Method to initialize the sliders with data from DataSingleton
    private void InitializeSliders()
    {
        // Check if data is available
        if (DS.GetData() != null && DS.GetData().CharacterData != null)
        {
            // Set the initial values of sliders based on CharacterData 
            movementSpeedSlider.value = DS.GetData().CharacterData.MovementSpeed;
            rotationSpeedSlider.value = DS.GetData().CharacterData.RotationSpeed;

            // Set the initial values as prevMovementSpeed and prevRotationSpeed
            prevMovementSpeed = movementSpeedSlider.value;
            prevRotationSpeed = rotationSpeedSlider.value;
        }
        else
        {
            Debug.LogWarning("Failed to initialize sliders: Data not loaded");

            // If CharacterData is not available, set the sliders' values based on PlayerPrefs
            movementSpeedSlider.value = PlayerPrefs.GetFloat("MovementSpeed", 0f);
            rotationSpeedSlider.value = PlayerPrefs.GetFloat("RotationSpeed", 0f);

            // Set the initial values as prevMovementSpeed and prevRotationSpeed
            prevMovementSpeed = movementSpeedSlider.value;
            prevRotationSpeed = rotationSpeedSlider.value;
        }

        UpdatePlayerPrefs("MovementSpeed", movementSpeedSlider.value);
        UpdatePlayerPrefs("RotationSpeed", rotationSpeedSlider.value);
    }

    void Update()
    {
        // Check if slider values have changed
        if (movementSpeedSlider.value != prevMovementSpeed)
        {
            UpdatePlayerPrefs("MovementSpeed", movementSpeedSlider.value);
            prevMovementSpeed = movementSpeedSlider.value;
        }

        if (rotationSpeedSlider.value != prevRotationSpeed)
        {
            UpdatePlayerPrefs("RotationSpeed", rotationSpeedSlider.value);
            prevRotationSpeed = rotationSpeedSlider.value;
        }
    }

    // Method to update PlayerPrefs with new slider values
    void UpdatePlayerPrefs(string key, float value)
    {
        // Update PlayerPrefs with new value
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
}