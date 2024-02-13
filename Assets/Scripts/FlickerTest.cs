using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerTest : MonoBehaviour
{
    // Two colors to indicate the refresh difference
    public Color startColor = Color.white;
    public Color endColor = Color.grey;

    // Range slider for the 'inspector' tool in unity to adjust speed
    [Range (0, 10)]
    public float speed = 1;

    // Initialize ren
    Renderer ren;

    void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    void Update()
    {
        ren.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }


}
