using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    private GameObject participant;
    private GameObject trans;
    private Transform respawnLocation;
    private Vector3 checkpointPosition;
    private bool checkpointSet = false;
    private Rigidbody participantRigidbody;

    private GameObject[] walls;

    void start()
    {
        walls = GameObject.FindGameObjectsWithTag("Cube");
    }

    public void Respawn()
    {
        participant = GameObject.FindGameObjectWithTag("Player");
        participantRigidbody = participant.GetComponent<Rigidbody>();
        var characterController = participant.GetComponent<CharacterController>();
        var playerMovementScript = participant.GetComponent<PlayerMovementWithKeyboard>();

        // Stop movement immediately
        if (playerMovementScript != null)
        {
            playerMovementScript.StopMovement();
        }

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        if (checkpointSet)
        {
            participant.transform.position = checkpointPosition;
        }
        else
        {
            trans = GameObject.FindGameObjectWithTag("Respawn");
            respawnLocation = trans.transform;
            participant.transform.position = respawnLocation.position;
        }

        // Reset velocity
        if (participantRigidbody != null)
        {
            participantRigidbody.velocity = Vector3.zero;
            participantRigidbody.angularVelocity = Vector3.zero;
        }

        if (characterController != null)
        {
            characterController.enabled = true;
        }

        // Resume movement
        if (playerMovementScript != null)
        {
            playerMovementScript.ResetMovement();
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
            participant = GameObject.FindGameObjectWithTag("Player");
            participantRigidbody = participant.GetComponent<Rigidbody>();
            var characterController = participant.GetComponent<CharacterController>();
            var playerMovementScript = participant.GetComponent<PlayerMovementWithKeyboard>();

            // Stop movement immediately
            if (playerMovementScript != null)
            {
                playerMovementScript.StopMovement();
            }

            if (characterController != null)
            {
                characterController.enabled = false;
            }

            if (checkpointSet)
            {
                participant.transform.position = checkpointPosition;
            }
            else
            {
                trans = GameObject.FindGameObjectWithTag("Respawn");
                respawnLocation = trans.transform;
                participant.transform.position = respawnLocation.position;
            }

            // Reset velocity
            if (participantRigidbody != null)
            {
                participantRigidbody.velocity = Vector3.zero;
                participantRigidbody.angularVelocity = Vector3.zero;
            }

            if (characterController != null)
            {
                characterController.enabled = true;
            }

            // Resume movement
            if (playerMovementScript != null)
            {
                playerMovementScript.ResetMovement();
            }
        }
    }
}
