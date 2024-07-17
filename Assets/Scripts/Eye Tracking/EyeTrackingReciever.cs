using UnityEngine;

public class EyeTrackingReceiver : MonoBehaviour
{
    public void ReceiveGazeData(string gazeDataJson)
    {
        GazeData gazeData = JsonUtility.FromJson<GazeData>(gazeDataJson);
        Debug.Log($"Gaze Data: X={gazeData.docX}, Y={gazeData.docY}");
        // Use the gaze data as needed
    }
}

[System.Serializable]
public class GazeData
{
    public int state;
    public float docX;
    public float docY;
    public long time;
}
