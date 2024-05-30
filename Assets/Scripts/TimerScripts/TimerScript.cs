using UnityEngine;
using TMPro;
using System.IO;
using data;
using trial;
using main;

/**
 * This class represents the script to handle the timer functionality, in which the a timer object is dragged into the timeText object, and this script
 * updates the time in the game to showcase in the UI for the user to indicate the time elapsed since they started a level. This script also provides
 * the functionality to store the values that are last captured before a user changes to the next level of the game, in which it stores this captured
 * value and store it into it's own respective json file. The separation of json files for each level ensures that when a new user runs a new game,
 * the values that were previously stored in the json file would then be over written with the new time values of a new game session.
 */
public class TimerScript : MonoBehaviour
{
    // Instance variables
    public float timeElapsed = 0;
    public bool timeIsRunning = true;

    // This is where the text object is dragged into in Unity
    public TMP_Text timeText;
    public AbstractTrial prevTrial;
    public AbstractTrial currentTrial;
    void Update()
    {
        prevTrial = currentTrial;
        currentTrial = Loader.Get().CurrTrial;

        // Time is checked if it is running on every frame
        if (timeIsRunning)
        {
            // If it is then keep adding time
            if (timeElapsed >= 0)
            {
                timeElapsed += Time.deltaTime;
                DisplayTime(timeElapsed);
            }
        }

        /*
         * This saves trial time information if a trial is finished, or if a trial is halted with it's endkey
         */
        if (prevTrial != null && (prevTrial.TrialID != currentTrial.TrialID))
        {
            outputJSON(prevTrial.TrialID, timeElapsed);
        }
    }

    // This method's functionality is to output the time by instantiating a TimerData object which contains the level and time
    private void outputJSON(int lvl, float timeEl)
    {
        // Create a TimerData object with the level name and time value
        TimerData timerData = new TimerData(lvl, timeEl);

        // Serialize the TimerData object to JSON
        string jsonData = JsonUtility.ToJson(timerData);

        // This overrides existing data in the json files, by writing new data captured from a game session
        File.WriteAllText(Application.dataPath + "/TimerOutputs/Timer" + lvl + ".json", jsonData);
    }

    // Class to hold timer data
    [System.Serializable]

    // This class represents a timer which is then extracted and saved into the json file as an object
    public class TimerData
    {
        public int level;
        public float time;

        public TimerData(int level, float time)
        {
            this.level = level;
            this.time = time;
        }
    }

    // This method displays the time in 00:00 format, it takes care of the formatting when the time reaches > 60 and turns it in to a minute.
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}