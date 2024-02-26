using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PS4Controller : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int hertz;
    private int frame;

    // Start is called before the first frame update
    void Start()
    {
        hertz = 0;
        frame = 0;

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        frame++;

        if (frame == 30) {

            frame = 0;

            if (Gamepad.all.Count > 0) 
            {
                if (Gamepad.all[0].leftStick.up.isPressed & hertz <= 59)
                {
                    hertz++;
                }
                else if (Gamepad.all[0].leftStick.down.isPressed & hertz >= 1)
                {
                    hertz--;
                }
            }
        }

        text.SetText(hertz.ToString() + " hertz");
    }
}
