using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(WaitForDeathAnimation(animator));
        }
    }

    public IEnumerator WaitForDeathAnimation(Animator playerAnimator){
        playerAnimator.SetBool("IsDead", true);

        // Wait for the death animation to finish
        while (!(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
                 playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)){
            yield return null;
        }
        playerAnimator.SetBool("IsDead", false);
        Respawn();
    }

    //Respawns the player from either the default respawn point or last picked up coin
    public void Respawn(){
        participant = GameObject.FindGameObjectWithTag("Player");
        participantRigidbody = participant.GetComponent<Rigidbody>();
        var characterController = participant.GetComponent<CharacterController>();

        if (checkpointSet){
            participant.transform.position = checkpointPosition;
        }
        else{
            respawnLocation = GameObject.FindGameObjectWithTag("Respawn").transform;
            participant.transform.position = respawnLocation.position;
        }

        if (characterController != null){
            characterController.enabled = true;
        }
    }

    public void SetCheckpoint(Vector3 position){
        checkpointPosition = new Vector3(position.x, 0.4f, position.z);
        checkpointSet = true;
    }
}
