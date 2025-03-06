using UnityEngine;

public class SettingsButtonHandler : MonoBehaviour
{
    public GameObject settingsMenuPrefab;  // Drag the Pause Menu UI here in the Inspector
    private bool isPaused = false;

    void Update()
    {
        // Check if the Escape key is pressed to toggle the settings menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu()
    {
        isPaused = !isPaused;

        if (settingsMenuPrefab == null)
        {
            Debug.LogError("🚨 settingsMenuPrefab is NOT assigned! Please assign it in the Inspector.");
            return;
        }

        if (isPaused)
        {
            Debug.Log("🛑 Game Paused - Showing Settings Menu");
            settingsMenuPrefab.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("▶ Game Resumed - Hiding Settings Menu");
            settingsMenuPrefab.SetActive(false);
            Time.timeScale = 1;  // Fix: Ensure the game resumes properly
        }
    }

    public void ContinueButton()
    {
        if (settingsMenuPrefab == null)
        {
            Debug.LogError("🚨 settingsMenuPrefab is NOT assigned! Please assign it in the Inspector.");
            return;
        }

        Debug.Log("▶ Continue Button Pressed - Resuming Game");
        isPaused = false;
        Time.timeScale = 1;
        settingsMenuPrefab.SetActive(false);
    }
}

