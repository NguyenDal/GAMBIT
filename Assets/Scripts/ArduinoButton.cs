using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButton : MonoBehaviour
{
    private SerialPort port;
    public bool input = false; // Exposed for other scripts
    private const int BAUD_RATE = 9600;
    private string buffer = "";

    private bool previousInput = false;  // To track changes

    private void Start()
    {
        // Automatically open COM6 on start
        string portName = "COM6"; // Change if your Arduino is on a different port

        port = new SerialPort(portName, BAUD_RATE);
        port.ReadTimeout = 200;  // Increased timeout
        port.DtrEnable = true;   // Needed for Nano 33 BLE Sense
        port.RtsEnable = true;   // Recommended for stability

        try
        {
            port.Open();
            port.DiscardInBuffer(); // Flush input buffer on open
            Debug.Log("Port Opened: " + portName);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Could not open port " + portName + ": " + e.Message);
        }
    }

    private void Update()
    {
        if (port != null && port.IsOpen)
        {
            try
            {
                string raw = port.ReadExisting();
                if (!string.IsNullOrEmpty(raw))
                {
                    buffer += raw;

                    while (buffer.Contains("\n"))
                    {
                        int newlineIndex = buffer.IndexOf("\n");
                        string data = buffer.Substring(0, newlineIndex).Trim();
                        buffer = buffer.Substring(newlineIndex + 1);

                        // Determine new input state based on data received
                        bool newInput = (data == "y");

                        // Only update and log if the state has changed
                        if (newInput != input)
                        {
                            input = newInput;
                            Debug.Log("Serial Data (Processed): " + data);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Serial read error: " + e.Message);
            }
        }
    }

    private void OnDestroy()
    {
        if (port != null && port.IsOpen)
            port.Close();
    }
}
