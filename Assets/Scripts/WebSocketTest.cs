// WebSocketTest.cs
using System;
using WebSocketSharp;
using UnityEngine;

public class WebSocketTest : MonoBehaviour
{
    private WebSocket ws;
    public FrequencyMovement freqMovement = null;
    private string currInput = "none";
    private bool isConnected = false;

    void Start()
    {
        InitializeWebSocket();
    }

    private void InitializeWebSocket()
    {
        freqMovement = gameObject.GetComponent<FrequencyMovement>();
        
        if (!(freqMovement != null && freqMovement.frequencyMovementEnabled))
        {
            Debug.LogWarning("FrequencyMovement not found or disabled");
            this.enabled = false;
            return;
        }
        
        ws = new WebSocket("ws://localhost:8765");
        
        ws.OnOpen += (sender, e) => {
            isConnected = true;
            Debug.Log("Connected to WebSocket server");
        };

        ws.OnError += (sender, e) => {
            Debug.LogError($"WebSocket error: {e.Message}");
            RetryConnection();
        };

        ws.OnClose += (sender, e) => {
            isConnected = false;
            Debug.Log("Disconnected from WebSocket server");
            RetryConnection();
        };

        ws.OnMessage += (sender, e) => {
            Debug.Log($"Received input: {e.Data}");
            currInput = e.Data;
        };

        ConnectToServer();
    }

    private void ConnectToServer()
    {
        try
        {
            Debug.Log("Attempting to connect to WebSocket server...");
            ws.Connect();
        }
        catch (Exception e)
        {
            Debug.LogError($"Connection failed: {e.Message}");
            RetryConnection();
        }
    }

    private void RetryConnection()
    {
        if (!isConnected && ws != null)
        {
            StartCoroutine(RetryConnectionCoroutine());
        }
    }

    private System.Collections.IEnumerator RetryConnectionCoroutine()
    {
        yield return new WaitForSeconds(2f);
        ConnectToServer();
    }

    private void Update()
    {
        if (isConnected)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (freqMovement == null) return;

        switch (currInput)
        {
            case "input_1":
                freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.forwardOffset);
                break;
            case "input_2":
                freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.backwardOffset);
                break;
            case "input_3":
                freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.leftOffset);
                break;
            case "input_4":
                freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.rightOffset);
                break;
            case "no_input":
                currInput = "none";
                break;
        }
    }

    void OnApplicationQuit()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
            Debug.Log("Closed WebSocket connection");
        }
    }
}
