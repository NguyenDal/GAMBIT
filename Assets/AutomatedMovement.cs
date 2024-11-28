using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DS = data.DataSingleton;
using UnityEngine.InputSystem;

public class AutomatedMovement : MonoBehaviour
{
    private Transform player;
    public float mouseSensitivity = 2f;
    private float movementSpeed; // Movement speed
    private bool isMoving = false; // Flag to track if player is currently moving
    private Vector3 targetPosition; // Target position to move towards
    private List<Transform> tiles; // List of all tiles on the map

    [SerializeField] Animator charaterAnimator;
    public GameObject tileMapObject; // Reference to the game object containing all tiles

    private bool newMovementSystem = false; // Assume false. Change later if toggle for 'Move with tiles' is selected before starting game

    private FrequencyMimicKeyboard frequencyKeyboardScript; // Script that controls frequency WASD movement
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Retrieve movement speed from PlayerPrefs, default to a value if not found
        movementSpeed = PlayerPrefs.GetFloat("MovementSpeed", 4f);

        if (movementSpeed > 4)
            movementSpeed = 4f;

        tiles = new List<Transform>();
        // Get all children of the tile map object and add them to list
        foreach (Transform child in tileMapObject.transform)
        {
            tiles.Add(child);
        }

        // Get any other movement scripts
        frequencyKeyboardScript = gameObject.GetComponent<FrequencyMimicKeyboard>();

        //If game is started with tile movement selected, set newMovementSystem true
        if (DS.GetData() != null && DS.GetData().CharacterData != null)
        {
            if ((PlayerPrefs.GetInt("MoveWithTiles" + "Enabled") == 1))
            {
                newMovementSystem = true;
            }
        }
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        if(Input.GetMouseButton(1)){
            // rotate the camera for the X axis
            player.Rotate(Vector3.up * inputX);
        }
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
                        StartCoroutine(MoveToTarget(new Vector3(nearestTile.position.x, player.position.y, nearestTile.position.z)));
                    }
                }
            }
        }
    }

    private void disableOtherMovementSystems() {
        frequencyKeyboardScript.enabled = false;
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
        charaterAnimator.SetBool("IsWalking", isMoving);

        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            // Move towards the target position using the retrieved movement speed
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

            // Stop movement if player has fallen off map
            if (transform.position.y < 0.1f)
            {
                isMoving = false;
                charaterAnimator.SetBool("IsWalking", isMoving);
                yield break;
            }

            yield return null;
        }

        isMoving = false;
        charaterAnimator.SetBool("IsWalking", isMoving);
    }
}