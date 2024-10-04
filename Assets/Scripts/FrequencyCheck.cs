using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyCheck : MonoBehaviour
{
    //Global float that will store the frequency that the user inputs in the main menu (prior to starting the first level.) 
    public static float frequency;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isFloat = float.TryParse(GameObject.Find("Input").GetComponent<Text>().text, out frequency);
        
        //If the entered frequency is not valid (should be a positive float value), we should not let the participant start the experiment.
        if (!isFloat || frequency <= 0)
        {
            //Let the participant know that their input is not valid, and prevent the start button from being clicked. 
            gameObject.GetComponent<Text>().text = "Not a valid frequency value.";
            startButton.interactable = false;
        }
        else
        {
            // Set the baseline frequency
            PlayerPrefs.SetFloat("BaselineFrequency", frequency);
            PlayerPrefs.Save();

            gameObject.GetComponent<Text>().text = "";
            startButton.interactable = true;
        }
    }
}
