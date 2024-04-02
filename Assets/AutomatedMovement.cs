using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomatedMovement : MonoBehaviour
{
    private float movementSpeed; // Movement speed
    private bool isMoving = false; // Flag to track if player is currently moving
    private Vector3 targetPosition; // Target position to move towards
    private List<Transform> tiles; // List of all tiles on the map

    public GameObject tileMapObject; // Reference to the game object containing all tiles

    private bool newMovementSystem = true; // Initialized newMovementSystem as true; currently a placeholder until toggle implementation

    private PlayerMovementWithKeyboard keyboardMovementScript; // Script that controlls WASD movement
    private GamepadController gamepadMovementScript; // Script that controlls Gamepad movement
    public GameObject HUDButtons; // Reference to the GameObject containing the HUD buttons used for player movement control
    public GameObject PS4Controller; // Reference to the GameObject containing the PS4ControllerMovement script (with hertz values)
    public GameObject HertzValues; // Reference to the GameObject displaying hertz values used for PS4 controller movement
    void Start()
    {
        // Retrieve movement speed from PlayerPrefs, default to a value if not found
        movementSpeed = PlayerPrefs.GetFloat("MovementSpeed", 5f);

        tiles = new List<Transform>();
        // Get all children of the tile map object and add them to list
        foreach (Transform child in tileMapObject.transform)
        {
            tiles.Add(child);
        }

        // Get the PlayerMovementWithKeyboard and GamepadController script components
        keyboardMovementScript = gameObject.GetComponent<PlayerMovementWithKeyboard>();
        gamepadMovementScript = gameObject.GetComponent<GamepadController>();
        
    }

    void Update()
    {
        if (newMovementSystem) {
            disableOtherMovementSystems();

            // Check if left mouse button is clicked and player is not already moving
            if (Input.GetMouseButtonDown(0) && !isMoving)
            {
                // Cast a ray from the mouse position into the game world
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Move toward clicked tile
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        Vector3 clickedTilePosition = hit.collider.transform.position;
                        Transform nearestTile = GetNearestTile(clickedTilePosition);
                        StartCoroutine(MoveToTarget(nearestTile.position));
                    }
                }
            }
        }
    }

    private void disableOtherMovementSystems() {
        keyboardMovementScript.enabled = false;
        gamepadMovementScript.enabled = false;
        HUDButtons.SetActive(false);
        PS4Controller.SetActive(false);
        HertzValues.SetActive(false);
    }

    // Find the nearest tile to a given position
    Transform GetNearestTile(Vector3 position)
    {
        Transform nearestTile = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform tile in tiles)
        {
            float distance = Vector3.Distance(tile.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            // Move towards the target position using the retrieved movement speed
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

            // Stop movement if player has fallen off map
            if (transform.position.y < 0.1f)
            {
                isMoving = false;
                yield break;
            }

            yield return null;
        }

        isMoving = false;
    }
}