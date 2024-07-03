using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    private GameObject participent;
    private GameObject trans;
    private Transform respawnLocation;
    private Vector3 checkpointPosition;
    private bool checkpointSet = false;

    private GameObject[] walls;

    void start()
    {
        walls = GameObject.FindGameObjectsWithTag("Cube");
    }

    public void Respawn()
    {
        participent = GameObject.FindGameObjectWithTag("Player");
        if (checkpointSet)
        {
            participent.transform.position = checkpointPosition;
        }
        else
        {
            trans = GameObject.FindGameObjectWithTag("Respawn");
            respawnLocation = trans.transform;
            participent.transform.position = respawnLocation.position;
        }
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        checkpointSet = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Participant"))
        {
            participent = GameObject.FindGameObjectWithTag("Player");
            if (checkpointSet)
            {
                participent.transform.position = checkpointPosition;
            }
            else
            {
                trans = GameObject.FindGameObjectWithTag("Respawn");
                respawnLocation = trans.transform;
                participent.transform.position = respawnLocation.position;
            }
        }
    }
}
