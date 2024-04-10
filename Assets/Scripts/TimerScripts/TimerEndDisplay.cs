using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;

/**
 * This class represents the functionality where it reads values from the json files that stored time values from all the levels,
 * in which how long the user took to complete each level, and this script overwrites the "00:00" text objects in the EndingScene
 * with the value stored in the timer files.
*/
public class TimerUI : MonoBehaviour
{
    // This is where the text object is dragged ie. L1-Text
    public TMP_Text timeText;

    void Start()
    {
        // Read the JSON file and update the UI text
        UpdateUIText();
    }

    void UpdateUIText()
    {
        // Extract level number from the text game object's name the ie. the 2nd char, L'1'-Text when the text object's name is L1-Text
        int levelNumber = (int)char.GetNumericValue(gameObject.name[1]);

        // Determine the file path based on the level name
        string filePath = Application.dataPath + "/TimerOutputs/Timer" + levelNumber + ".json";
        
        
        // Read the JSON content
        string json = File.ReadAllText(filePath);

        // Deserialize JSON to TimerData object, extracts the time from the file
        TimerScript.TimerData timerData = JsonConvert.DeserializeObject<TimerScript.TimerData>(json);

        // Update the UI text with the time value
        float minutes = Mathf.FloorToInt(timerData.time / 60);
        float seconds = Mathf.FloorToInt(timerData.time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

