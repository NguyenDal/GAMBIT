using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoundary : MonoBehaviour
{
    public GameObject floorAndConnectorObject; // Assign FloorAndConnectorObject in the inspector
    private GameObject[] floorAndConnectorCubes;
    private bool withinBoundaries = true;
    private Vector3 previousPosition;
    private bool safeMode = false;

    void Start()
    {
        previousPosition = transform.position;

        // Load the value of the safe mode toggle from PlayerPrefs
        safeMode = PlayerPrefs.GetInt("SafeModeEnabled", 0) == 1;

        // Populate the FloorAndConnectorCubes array
        PopulateFloorAndConnectorCubes();
    }

    void PopulateFloorAndConnectorCubes()
    {
        List<GameObject> cubesList = new List<GameObject>();

        // Check if FloorAndConnectorObject is assigned
        if (floorAndConnectorObject != null)
        {
            // Find children of FloorAndConnectorObject tagged with "Floor" or "Connector"
            foreach (Transform child in floorAndConnectorObject.transform)
            {
                if (child.CompareTag("Floor") || child.CompareTag("Connector"))
                {
                    // Add children's children (cubes) to the list
                    foreach (Transform cubeChild in child)
                    {
                        cubesList.Add(cubeChild.gameObject);
                    }
                }
            }
        }

        // Convert list to array
        floorAndConnectorCubes = cubesList.ToArray();
    }

    void Update()
    {
        // if safe mode is true, start the experiment (only level 0) with invisible walls activated.
        if (safeMode)
        {
            withinBoundaries = false;

            foreach (GameObject obj in floorAndConnectorCubes)
            {
                // Check if the parent object is activated (connector activation script doesn't set individual cubes as inactive)
                if (obj.transform.parent.gameObject.activeSelf)
                {
                    float minX = obj.transform.position.x - obj.transform.localScale.x / 2;
                    float maxX = obj.transform.position.x + obj.transform.localScale.x / 2;
                    float minZ = obj.transform.position.z - obj.transform.localScale.z / 2;
                    float maxZ = obj.transform.position.z + obj.transform.localScale.z / 2;

                    if (transform.position.x >= minX && transform.position.x <= maxX &&
                        transform.position.z >= minZ && transform.position.z <= maxZ)
                    {
                        withinBoundaries = true;
                        break; // Exit the loop if the character is within the boundaries of any activated object
                    }
                }
            }

            if (!withinBoundaries)
            {
                transform.position = previousPosition; // Reset character position to previous position
            }

            // Update previousPosition for next frame
            previousPosition = transform.position;
        }
    }
}
