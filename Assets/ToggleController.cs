using UnityEngine;
using UnityEngine.UI;
using data;
using DS = data.DataSingleton;

public class ToggleController : MonoBehaviour
{
    public Toggle firstPersonToggle;

    private bool prevFirstPersonState;

    private void Start()
    {
        // Subscribe to the DataLoaded event
        DataSingleton.DataLoaded += InitializeToggle;

        // Set the initial value of the toggle based on data or PlayerPrefs
        InitializeToggle();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the DataLoaded event to prevent memory leaks
        DataSingleton.DataLoaded -= InitializeToggle;
    }

    // Method to initialize the toggle with data from DataSingleton
    private void InitializeToggle()
    {
        // Check if data is available
        if (DS.GetData() != null && DS.GetData().CharacterData != null)
        {
            // Set the initial value of the toggle based on CharacterData 
            bool isFirstPersonData = DS.GetData().CharacterData.FirstPerson;

            // Load the initial value from PlayerPrefs
            bool isFirstPersonEnabled = PlayerPrefs.GetInt("FirstPersonEnabled", isFirstPersonData ? 1 : 0) == 1;

            // Save the correct initial value to PlayerPrefs
            SaveToggleState(isFirstPersonData);

            firstPersonToggle.isOn = isFirstPersonData;
        }
        else
        {
            // If CharacterData is not available, set the toggle value based on PlayerPrefs
            firstPersonToggle.isOn = PlayerPrefs.GetInt("FirstPersonEnabled", 0) == 1;
        }

        // Set the initial value as prevFirstPersonState
        prevFirstPersonState = firstPersonToggle.isOn;
    }

    void Update()
    {
        // Check if toggle value has changed
        if (firstPersonToggle.isOn != prevFirstPersonState)
        {
            ToggleFirstPerson();
        }
    }

    void ToggleFirstPerson()
    {
        // Save the value of the toggle to PlayerPrefs
        SaveToggleState(firstPersonToggle.isOn);

        // Update the prevFirstPersonState
        prevFirstPersonState = firstPersonToggle.isOn;

        // Debug log to verify toggle state change
        Debug.Log("Toggle state changed to: " + firstPersonToggle.isOn);
    }

    // Method to save the toggle state to PlayerPrefs
    void SaveToggleState(bool value)
    {
        int isEnabled = value ? 1 : 0;
        PlayerPrefs.SetInt("FirstPersonEnabled", isEnabled);
        PlayerPrefs.Save(); // Make sure to save changes
    }
}