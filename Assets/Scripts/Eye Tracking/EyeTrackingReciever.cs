using UnityEngine;
using System.IO;

public class EyeTrackingReciever : MonoBehaviour
{
    private ArrowMovementConfig config; 
    private GazeData _latestGazeData;

    private float cellWidth;
    private float cellHeight;
    public float movementSpeed = 5f; // Speed for movement
    public GameObject player; // Reference to the player's camera

    public RectTransform forwardArrow, leftArrow, rightArrow, backwardArrow;
    
    public void Start()
    {
        // Path to Config_ArrowMovement.json file
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

    public void AdjustArrowPosition()
    {
        Vector2 center = new Vector2(config.screenWidth / 2, config.screenHeight / 2);
        float offset = config.arrowSpacing;

        // Set positions and sizes for arrows
        forwardArrow.anchoredPosition = center + new Vector2(0, offset);
        forwardArrow.sizeDelta = new Vector2(config.arrowSize, config.arrowSize);

        backwardArrow.anchoredPosition = center + new Vector2(0, -offset);
        backwardArrow.sizeDelta = new Vector2(config.arrowSize, config.arrowSize);

        leftArrow.anchoredPosition = center + new Vector2(-offset, 0);
        leftArrow.sizeDelta = new Vector2(config.arrowSize, config.arrowSize);

        rightArrow.anchoredPosition = center + new Vector2(offset, 0);
        rightArrow.sizeDelta = new Vector2(config.arrowSize, config.arrowSize);
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
        if (IsWitinArrow(gazePosition, forwardArrow))
        {
            MoveForward();
        }
        else if (IsWitinArrow(gazePosition, backwardArrow))
        {
            MoveBackward();
        }
        else if (IsWitinArrow(gazePosition, leftArrow))
        {
            MoveLeft();
        }
        else if (IsWitinArrow(gazePosition, rightArrow))
        {
            MoveRight();
        }
    }

    private bool IsWitinArrow(Vector2 gazePosition , RectTransform arrow) 
    {
        Vector2 arrowPositon = arrow.anchoredPosition;
        Vector2 arrowSize = arrow.sizeDelta;

        return gazePosition.x > arrowPositon.x - arrowSize.x / 2 &&
               gazePosition.x < arrowPositon.x + arrowSize.x / 2 &&
               gazePosition.y > arrowPositon.y - arrowSize.y / 2 &&
               gazePosition.y < arrowPositon.y + arrowSize.y / 2;
    }

    private void MoveForward()
    {
        Debug.Log("Moving Forward");
        player.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void MoveBackward()
    {
        Debug.Log("Moving Backward");
        player.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        Debug.Log("Moving Left");
        player.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
    private void MoveRight()
    {
        Debug.Log("Moving Right");
        player.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }


    public GazeData GetGazeData()
    {
        return _latestGazeData;
    }

    private ArrowMovementConfig LoadConfig(string filePath) // Loads arrow movement configuration from a JSON file or initializes default settings if the file is missing.
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
    public float arrowSize; //  djustment for size of the arrows
    public float arrowSpacing; // Spacing between arrows
}
