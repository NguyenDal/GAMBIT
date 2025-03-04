using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;


public class ardunioButton : MonoBehaviour
{
    public Image imgs;
    public float frequency = 1f;

    SerialPort dataStream = new SerialPort("COM3", 9600);
    // Start is called before the first frame update
    void Start()
    {
        dataStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if(dataStream.ReadLine() != null){
            changeButtonColour();
        }
    }

    void changeButtonColour(){
        imgs.color = Color.Lerp(Color.grey, Color.red, Mathf.PingPong(Time.time * frequency, 0.5f));
    }
}
