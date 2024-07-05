using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class respawn : MonoBehaviour
{
    private GameObject participent;
    private GameObject trans;
    private Transform respawnLocation;

    private GameObject[] walls;

    public Animator animator;

    void start(){
        walls = GameObject.FindGameObjectsWithTag("Cube");
    }
    public void Respawn(){
        participent = GameObject.FindGameObjectWithTag("Player");
        trans = GameObject.FindGameObjectWithTag("Respawn");
        respawnLocation = trans.transform;
        participent.transform.position = respawnLocation.position;
    }
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.name.Equals("Participant")){
            StartCoroutine(WaitForDeathAnimation());
        }
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
}
