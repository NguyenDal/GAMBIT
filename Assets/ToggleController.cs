using UnityEngine;
using UnityEngine.UI;
using data;
using DS = data.DataSingleton;

// Attach this script to the toggle UI component that controls either the first-person or safe mode functionality
// Set the "Data Key" field in the inspector to "FirstPerson" for first-person functionality or "SafeMode" for safe mode functionality

public class ToggleController : MonoBehaviour
{
    public Toggle toggle;
    public string dataKey;
    private bool previousState;

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
            // Set the initial value of the toggle based on the data
            bool toggleState;
            switch (dataKey)
            {
                case "FirstPerson":
                    toggleState = DS.GetData().CharacterData.FirstPerson;
                    break;
                case "SafeMode":
                    toggleState = DS.GetData().CharacterData.SafeMode;
                    break;
                default://Default to MoveWithTiles, shouldn't land here without reason though
                    toggleState = DS.GetData().CharacterData.MoveWithTiles;
                    break;
            }

            // Load the initial value from PlayerPrefs
            bool isEnabled = PlayerPrefs.GetInt(dataKey + "Enabled", toggleState ? 1 : 0) == 1;

            // Save the correct initial value to PlayerPrefs
            SaveToggleState(toggleState);

            toggle.isOn = toggleState;
        }
        else
        {
            // If data is not available, set the toggle value based on PlayerPrefs
            toggle.isOn = PlayerPrefs.GetInt(dataKey + "Enabled", 0) == 1;
        }

        // Set the initial value as previousState
        previousState = toggle.isOn;
    }

    void Update()
    {
        // Check if toggle value has changed
        if (toggle.isOn != previousState)
        {
            ToggleSetting();
        }
    }

    void ToggleSetting()
    {
        // Save the value of the toggle to PlayerPrefs
        SaveToggleState(toggle.isOn);

        // Update the previousState
        previousState = toggle.isOn;

        // Debug log to verify toggle state change
        Debug.Log("Toggle state changed to: " + toggle.isOn);
    }

    // Method to save the toggle state to PlayerPrefs
    void SaveToggleState(bool value)
    {
        int isEnabled = value ? 1 : 0;
        PlayerPrefs.SetInt(dataKey + "Enabled", isEnabled);
        PlayerPrefs.Save(); // Make sure to save changes
    }
}