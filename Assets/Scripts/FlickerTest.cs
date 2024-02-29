using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerTest : MonoBehaviour
{
    // Two colors to indicate the refresh difference
    public Color startColor = Color.white;
    public Color endColor = Color.grey;
    public GameObject g1;

    // Range slider for the 'inspector' tool in unity to adjust speed
    [Range (0, 10)]
    public float speed = 1;

    // Initialize ren
    Renderer ren;

    void Awake()
    {
        ren = GetComponent<Renderer>();
        if (g1.name == "Forward-Text")
        {
            speed = 5;
            ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
        if (g1.name == "Back-Text")
        {
            speed = 1;
            ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
        if (g1.name == "Left-Text")
        {
            speed = 4;
            ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
        if (g1.name == "Right-Text")
        {
            speed = 7;
            ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
    }

    void Update()
    {
        ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }


}
