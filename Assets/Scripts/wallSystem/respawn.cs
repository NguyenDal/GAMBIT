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

    public Animator animator;

    void start(){
        walls = GameObject.FindGameObjectsWithTag("Cube");
    }

    void OnTriggerEnter(Collider other){
        if (other.tag != "EnemyAggroRange" && other.tag != "Pickup"){
            StartCoroutine(WaitForDeathAnimation());
        }
    }

    IEnumerator WaitForDeathAnimation(){
        // Play the death animation
        animator.SetBool("IsDead", true);

        // Wait for the death animation to finish
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (!(stateInfo.IsName("Death") && stateInfo.normalizedTime >= 1.0f)){
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Reset the "IsDead" parameter
        animator.SetBool("IsDead", false);

        // Respawn the player
        Respawn();
    }

    public void Respawn()
    {
        participant = GameObject.FindGameObjectWithTag("Player");
        participantRigidbody = participant.GetComponent<Rigidbody>();
        var characterController = participant.GetComponent<CharacterController>();

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

    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = new Vector3(position.x, (float)0.4, position.z);
        checkpointSet = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // TODO: GAMBIT-143 Needs to be removed 
        if (collision.gameObject.name.Equals("Participant") && false)
        {
            if (collision.gameObject.name.Equals("Participant")){
                StartCoroutine(WaitForDeathAnimation());
            }

            participant = GameObject.FindGameObjectWithTag("Player");
            participantRigidbody = participant.GetComponent<Rigidbody>();
            var characterController = participant.GetComponent<CharacterController>();
            var playerMovementScript = participant.GetComponent<PlayerMovementWithKeyboard>();

            // Stop movement immediately
            if (playerMovementScript != null){
                playerMovementScript.StopMovement();
            }

            if (characterController != null){
                characterController.enabled = false;
            }

            if (checkpointSet){
                participant.transform.position = checkpointPosition;
            }
            else{
                trans = GameObject.FindGameObjectWithTag("Respawn");
                respawnLocation = trans.transform;
                participant.transform.position = respawnLocation.position;
            }

            // Reset velocity
            if (participantRigidbody != null){
                participantRigidbody.velocity = Vector3.zero;
                participantRigidbody.angularVelocity = Vector3.zero;
            }

            if (characterController != null){
                characterController.enabled = true;
            }

            // Resume movement
            if (playerMovementScript != null){
                playerMovementScript.ResetMovement();
            }
        }
    }
}
