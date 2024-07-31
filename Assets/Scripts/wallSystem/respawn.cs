using System.Collections;
using UnityEngine;

public class respawn : MonoBehaviour{
    private GameObject participant;
    private Transform respawnLocation;
    private Vector3 checkpointPosition;
    private bool checkpointSet = false;
    private Rigidbody participantRigidbody;
    public Animator animator;

    void Start(){
        GameObject.FindGameObjectsWithTag("Cube");
    }

    //If the player hits something that should kill it, kill the player
    void OnTriggerEnter(Collider other){
        if (other.tag != "EnemyAggroRange" && other.tag != "Pickup"){
            StartCoroutine(WaitForDeathAnimation());
        }
    }

    IEnumerator WaitForDeathAnimation(){
        animator.SetBool("IsDead", true);

        // Wait for the death animation to finish
        while (!(animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && 
                 animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)){
            yield return null;
        }
        animator.SetBool("IsDead", false);
        Respawn();
    }

    //Respawns the player from either the default respawn point or last picked up coin
    public void Respawn(){
        participant = GameObject.FindGameObjectWithTag("Player");
        participantRigidbody = participant.GetComponent<Rigidbody>();
        var characterController = participant.GetComponent<CharacterController>();
        var playerMovementScript = participant.GetComponent<PlayerMovementWithKeyboard>();

        StopMovement(playerMovementScript, characterController);

        if (checkpointSet){
            participant.transform.position = checkpointPosition;
        }
        else{
            respawnLocation = GameObject.FindGameObjectWithTag("Respawn").transform;
            participant.transform.position = respawnLocation.position;
        }
        ResetVelocity(participantRigidbody);

        if (characterController != null){
            characterController.enabled = true;
        }
        ResumeMovement(playerMovementScript);
    }

    //Prevents the player from continuing to move during death
    private void StopMovement(PlayerMovementWithKeyboard playerMovementScript, CharacterController characterController){
        playerMovementScript?.StopMovement();
        if (characterController != null){
            characterController.enabled = false;
        }
    }

    //Prevents the player from continuing to move during death
    private void ResetVelocity(Rigidbody rigidbody){
        if (rigidbody != null){
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void ResumeMovement(PlayerMovementWithKeyboard playerMovementScript){
        playerMovementScript?.ResetMovement();
    }

    public void SetCheckpoint(Vector3 position){
        checkpointPosition = new Vector3(position.x, 0.4f, position.z);
        checkpointSet = true;
    }
}