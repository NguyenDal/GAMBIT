using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpListener : MonoBehaviour
{
    [SerializeField] int listenPort;
    private UdpClient udpClient;
    private bool isListening;

    void Start()
    {
        if (gameObject.GetComponent<FrequencyMovement>() != null && gameObject.GetComponent<FrequencyMovement>().frequencyMovementEnabled)
        {
            StartListening();
        }
    }

    private void StartListening()
    {
        udpClient = new UdpClient(listenPort);
        isListening = true;
        Debug.Log($"Listening for UDP messages on port {listenPort}");
        StartCoroutine(ListenForMessages());
    }

    private IEnumerator ListenForMessages()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, listenPort);

        while (isListening)
        {
            if (udpClient.Available > 0)
            {
                try
                {
                    var receivedResult = udpClient.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedResult);

                    if (int.TryParse(receivedMessage, out int frequency))
                    {
                        gameObject.GetComponent<FrequencyMovement>().UpdateFrequency(frequency);
                    }
                    else
                    {
                        Debug.Log("Received unparseable UDP message: " + receivedMessage);
                    }
                }
                catch (SocketException ex)
                {
                    Debug.LogError($"Socket error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error receiving UDP message: {ex.Message}");
                }
            }

            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds before the next check
        }
    }


    private void OnApplicationQuit()
    {
        isListening = false;
        udpClient.Close();
        Debug.Log("Stopped listening for UDP messages.");
    }
}
