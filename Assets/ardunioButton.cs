using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ardunioButton : MonoBehaviour
{
    public Image imgs;
    public float frequency = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeButtonColour(){
        imgs.color = Color.Lerp(Color.grey, Color.red, Mathf.PingPong(Time.time * frequency, 0.5f));
    }
}
