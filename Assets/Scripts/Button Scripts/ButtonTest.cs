using UnityEngine;
using UnityEngine.UI;


/*
 * This class represents the script for the flickering color for the buttons
 * on the UI HUD, a slider is created to manually change the speed for each
 * button inside unity as well.
 */
public class ButtonTest : MonoBehaviour
{

    // Color objects
    public Color startColor = Color.white;
    public Color endColor = Color.grey;

    // Range slider
    [Range(0, 10)]
    public float speed = 1;

    // Image object of button
    Image imgs;


    void Awake()
    {
        imgs = GetComponent<Image>();
    }

    void Update()
    {
        // Calculation for changing the color with time
        imgs.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time* speed, 1));
    }

}