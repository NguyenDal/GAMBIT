using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EyeTrackingReciever : MonoBehaviour
{
    private EyeTrackingConfig config;
    private GazeData _latestGazeData;

    private float cellWidth;
    private float cellHeight;
    public float movementSpeed = 5f; // Speed for movement

    private void Start()
    {
        // Adjust the configuration file path to match the correct directory
        string filePath = Path.Combine(Application.dataPath, "../Configuration_Files/EyeTrackingConfig.json");
        config = ConfigLoader.LoadConfig(filePath);

        if (config != null)
        {
            cellWidth = config.screenWidth / config.gridCols;
            cellHeight = config.screenHeight / config.gridRows;
        }
        else
        {
            Debug.LogError("Failed to load EyeTrackingConfig.json.");
        }
    }

    public void ReceiveGazeData(string gazeDataJson)
    {
        _latestGazeData = JsonUtility.FromJson<GazeData>(gazeDataJson);
        Debug.Log($"Gaze Data: X={_latestGazeData.docX}, Y={_latestGazeData.docY}");

        Vector2 gazePosition = new Vector2(_latestGazeData.docX, _latestGazeData.docY);
        HandleGazePosition(gazePosition);
    }

    private void HandleGazePosition(Vector2 gazePosition)
    {
        int row = Mathf.FloorToInt(gazePosition.y / cellHeight);
        int col = Mathf.FloorToInt(gazePosition.x / cellWidth);

        Debug.Log($"Grid Position: [{row}, {col}]");

        switch (row)
        {
            case 0:
                if (col == 1)
                {
                    MoveForward();
                }
                else if (col == 2)
                {
                    OpenSettingsMenu();
                }
                break;
            case 1:
                if (col == 0)
                {
                    MoveLeft();
                }
                else if (col == 2)
                {
                    MoveRight();
                }
                else if (col == 1)
                {
                    PerformAction();
                }
                break;
            case 2:
                if (col == 1)
                {
                    MoveBackward();
                }
                break;
        }
    }

    private void MoveForward()
    {
        Debug.Log("Moving Forward");
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        Debug.Log("Moving Left");
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    private void MoveRight()
    {
        Debug.Log("Moving Right");
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    private void MoveBackward()
    {
        Debug.Log("Moving Backward");
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }

    private void PerformAction()
    {
        Debug.Log("Performing Action");
        // Implement action logic, e.g., trigger animation
    }

    private void OpenSettingsMenu()
    {
        Debug.Log("Opening Settings Menu");
        // Implement settings menu logic, e.g., show settings menu
    }

    public GazeData GetGazeData()
    {
        return _latestGazeData;
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

[System.Serializable]
public class EyeTrackingConfig
{
    public float screenWidth;
    public float screenHeight;
    public int gridCols;
    public int gridRows;
}

public static class ConfigLoader
{
    public static EyeTrackingConfig LoadConfig(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<EyeTrackingConfig>(json);
        }
        Debug.LogError("Configuration file not found: " + filePath);
        return null;
    }
}
