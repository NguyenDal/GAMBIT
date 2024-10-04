using UnityEngine;
using System.IO;

public class EyeTrackingReciever : MonoBehaviour
{
    private ArrowMovementConfig config; // Update the config class to match your settings
    private GazeData _latestGazeData;

    private float cellWidth;
    private float cellHeight;
    public float movementSpeed = 5f; // Speed for movement
    public Camera playerCamera; // Reference to the player's camera

    private void Start()
    {
        // Path to your Config_ArrowMovement.json file
        string filePath = Path.Combine(Application.dataPath, "../Configuration_Files/Config_ArrowMovement.json");
        config = LoadConfig(filePath);

        if (config != null)
        {
            cellWidth = config.screenWidth / 3; // Divide the screen into 3 columns
            cellHeight = config.screenHeight; // Full screen height
        }
        else
        {
            Debug.LogError("Failed to load configuration.");
        }
    }

    public void ReceiveGazeData(GazeData gazeData)
    {
        _latestGazeData = gazeData;
        Debug.Log($"Gaze Data: X={_latestGazeData.x}, Y={_latestGazeData.y}");

        Vector2 gazePosition = new Vector2(_latestGazeData.x, _latestGazeData.y);
        HandleGazePosition(gazePosition);
    }

    private void HandleGazePosition(Vector2 gazePosition)
    {
        int col = Mathf.FloorToInt(gazePosition.x / cellWidth);

        Debug.Log($"Gaze Column: {col}");

        switch (col)
        {
            case 0:
                MoveCameraLeft();
                break;
            case 1:
                CenterCamera();
                break;
            case 2:
                MoveCameraRight();
                break;
        }
    }

    private void MoveCameraLeft()
    {
        Debug.Log("Moving Camera Left");
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime); // Use movement settings
        playerCamera.transform.localEulerAngles = new Vector3(0, -config.cameraAngle, 0); // Adjust the angle from config
    }

    private void MoveCameraRight()
    {
        Debug.Log("Moving Camera Right");
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime); // Use movement settings
        playerCamera.transform.localEulerAngles = new Vector3(0, config.cameraAngle, 0); // Adjust the angle from config
    }

    private void CenterCamera()
    {
        Debug.Log("Centering Camera");
        transform.Translate(Vector3.zero); // Stop movement
        playerCamera.transform.localEulerAngles = Vector3.zero;
    }

    public GazeData GetGazeData()
    {
        return _latestGazeData;
    }

    private ArrowMovementConfig LoadConfig(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ArrowMovementConfig>(json);
        }
        Debug.LogError("Configuration file not found: " + filePath);
        return null;
    }
}

[System.Serializable]
public class GazeData
{
    public float x;
    public float y;
}

[System.Serializable]
public class ArrowMovementConfig
{
    public float screenWidth;
    public float screenHeight;
    public float cameraAngle; // Angle adjustment for camera movement
}
