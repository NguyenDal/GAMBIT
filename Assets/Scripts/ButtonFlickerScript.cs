using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlickerScript : MonoBehaviour
{
    // Two colors to indicate the refresh difference
    public Color startColor = Color.white;
    public Color endColor = Color.grey;

    // Range slider for the 'inspector' tool in unity to adjust speed
    [Range (0, 10)]
    public float speed = 1;

    // Initialize Renderer
    Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        img.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }


}
