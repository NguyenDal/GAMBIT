using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFlicker : MonoBehaviour
{
    // Color objects
    public Color startColor = Color.red;
    public Color endColor = Color.yellow;

    public float frequency = 1;

    // Image object of button
    Image imgs;

    // Reference to the TextMeshProUGUI for displaying frequency
    [SerializeField] private TextMeshProUGUI frequencyText;

    void Awake()
    {
        imgs = GetComponent<Image>();
    }

    void Update()
    {
        // Update the frequency text in real-time
        if (frequencyText != null)
        {
            frequencyText.text = string.Format("{0:0.0} Hz", frequency);
        }
    }

    // When user moves up
    public void InputUp()
    {
        imgs.color = Color.Lerp(startColor, Color.blue, Mathf.PingPong(Time.time * frequency, 0.5f));
        // print("up");
    }

    public void ResetColour()
    {
        imgs.color = Color.Lerp(startColor, startColor, Mathf.PingPong(Time.time * frequency, 1f));
    }

    public void UpdateFrequency(float value)
    {
        frequency = value;
    }
}