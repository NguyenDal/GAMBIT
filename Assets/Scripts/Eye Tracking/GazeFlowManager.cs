using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GazeFlowManager : MonoBehaviour
{
    public string apiKey = "AppKeyTrial"; 
    public float requestInterval = 0.1f; // Interval between API requests in seconds
    public EyeTrackingReciever eyeTrackingReceiver; // Reference to EyeTrackingReciever

    private void Start()
    {
        if (eyeTrackingReceiver == null)
        {
            Debug.LogError("EyeTrackingReceiver not assigned!");
            return;
        }

        StartCoroutine(GetGazeData());
    }

    private IEnumerator GetGazeData()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get("https://api.gazerecorder.com/gaze?apiKey=" + apiKey);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching gaze data: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                GazeData gazeData = JsonUtility.FromJson<GazeData>(json);
                eyeTrackingReceiver.ReceiveGazeData(gazeData); // Forward gaze data to EyeTrackingReciever
            }

            yield return new WaitForSeconds(requestInterval);
        }
    }
}