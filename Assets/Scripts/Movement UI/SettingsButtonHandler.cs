using UnityEngine;

public class SettingsButtonHandler : MonoBehaviour
{
    public GameObject settingsMenuPrefab;
    private bool isPaused = false;


    void Update()
    {
        // Check if the Escape key is pressed to toggle settings menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Stop timers or game processes
            Time.timeScale = 0;
            settingsMenuPrefab.SetActive(true);
        }
        else
        {
            // Resume timers or game processes
            Time.timeScale = 1;
            settingsMenuPrefab.SetActive(false);
        }
    }
    public void ContinueButton()
    {
        if(!isPaused)
        {
            // Resume timers or game processes
            Time.timeScale = 1;
            settingsMenuPrefab.SetActive(false);

        }
    }
}
