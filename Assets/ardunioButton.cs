using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
 
public class arduinoButton : MonoBehaviour
{
    // Hard-coded serial port and baud rate for simplicity.
    // Make sure these match your Arduino setup (Serial.begin(115200) for example).
    private SerialPort dataStream = new SerialPort("COM6", 115200);
 
    // Reference to the UI Image you want to change.
    public Image buttonImage;
 
    void Start()
    {
        try
        {
            dataStream.Open();
            dataStream.ReadTimeout = 100;  // optional timeout
            Debug.Log("Serial port opened successfully on "
                      + dataStream.PortName + " at " + dataStream.BaudRate + " baud.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }
 
    void Update()
    {
        // Check if the port is valid and open
        if (dataStream != null && dataStream.IsOpen)
        {
            try
            {
                // Optional: check if there's data available before reading
                if (dataStream.BytesToRead > 0)
                {
                    // Read one line from the serial buffer
                    string line = dataStream.ReadLine();
                    Debug.Log("Read from serial: " + line);
 
                    // Convert it to an integer (0 or 1 from our Arduino code)
                    int buttonState = int.Parse(line);
                    Debug.Log("Parsed buttonState: " + buttonState);
 
                    // 0 -> not pressed, 1 -> pressed
                    if (buttonState == 0)
                    {
                        // Not pressed -> grey
                        buttonImage.color = Color.grey;
                        Debug.Log("Button not pressed -> Image is grey");
                    }
                    else
                    {
                        // Pressed -> red
                        buttonImage.color = Color.red;
                        Debug.Log("Button pressed -> Image is red");
                    }
                }
            }
            catch (System.TimeoutException tex)
            {
                Debug.LogWarning("Serial read timeout: " + tex.Message);
            }
            catch (System.FormatException fex)
            {
                Debug.LogWarning("Data format error (couldn't parse to int): " + fex.Message);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Serial read error: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Serial port not open or not assigned.");
        }
    }
 
    void OnApplicationQuit()
    {
        // Make sure to close the port when the application ends
        if (dataStream != null && dataStream.IsOpen)
        {
            dataStream.Close();
            Debug.Log("Serial port closed.");
        }
    }
}