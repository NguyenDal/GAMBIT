using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


/*
 * This class represents the script for the flickering color for the buttons
 * on the UI HUD, a slider is created to manually change the speed for each
 * button inside unity as well.
 */
public class ButtonFlicker : MonoBehaviour
{

    // Color objects
    public Color startColor = Color.white;
    public Color endColor = Color.grey;

    public float frequency = 1;

    // Image object of button
    Image imgs;
    //button game objects
    GameObject forwardButton;
    GameObject backButton;
    //[SerializeField] GameObject leftButton;
    //[SerializeField] GameObject rightButton;
    Image forwardButtonImage;


    void Awake()
    {
        imgs = GetComponent<Image>();
        forwardButton = GameObject.Find("Forward-Button");
        forwardButtonImage = forwardButton.GetComponent<Image>();
        
    }

    void Update()
    {
        // Calculation for changing the color with time
        //imgs.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * frequency, 0.5f));
    }

    //when user moves up
    public void inputUp(){
        imgs.color = Color.Lerp(startColor, Color.blue, Mathf.PingPong(Time.time * frequency, 0.5f));
        print("up");
    }
    public void resetColour(){
        imgs.color = Color.Lerp(startColor, startColor, Mathf.PingPong(Time.time * frequency, 0.5f));
    }
    public void UpdateFrequency(float value){
        frequency = value;
    }
}
