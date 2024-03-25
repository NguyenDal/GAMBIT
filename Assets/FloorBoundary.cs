using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoundary : MonoBehaviour
{
    public GameObject[] floorObjects; // Assign cubes and connectors to this array in the inspector
    private bool withinBoundaries = true;
    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        withinBoundaries = false;

        foreach (GameObject obj in floorObjects)
        {
            // Check if the object is activated
            if (obj.activeSelf)
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