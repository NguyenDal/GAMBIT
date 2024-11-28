using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 60f;
    public bool timerIsRunning = false;
    public TMP_Text timerText;
    public bool onSceneChange = false;

    void Start()
    {

        timerIsRunning = true;
        UpdateTimerDisplay(timeRemaining);
    }

    void Update()
    {

        if (timerIsRunning)
        {

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {

                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(timeRemaining);
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);


        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void OnSceneChangede()
    {
        Debug.Log("This happened");
        onSceneChange = true;
        PlayerPrefs.SetFloat("SavedTime", timeRemaining); // Save the timer value
        PlayerPrefs.Save();
        Debug.Log($"Timer value saved: {timeRemaining} seconds");
    }

  

}
