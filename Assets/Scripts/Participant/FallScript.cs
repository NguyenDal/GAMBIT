using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour {
    public Rigidbody playerRigidBody;

    void Start() {
        GetComponent<CharacterController>().enabled = true;
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        GetComponent<CharacterController>().enabled = true;
    }

    private void OnCollisionStay(Collision collision) {
        var characterController = GetComponent<CharacterController>();
        characterController.enabled = true;

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) {
            if (!characterController.enabled) {
                if (characterController.isGrounded) {
                    Debug.Log("Player still on map, but unable to move");
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        var characterController = GetComponent<CharacterController>();

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) {
            characterController.enabled = false;
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d")) {
            if (!characterController.enabled && characterController.isGrounded) {
                Debug.Log("Key released and grounded.");
                characterController.enabled = true;
            }
        }

        if (collision.gameObject.CompareTag("Wall")) {
            if (characterController.isGrounded) {
                Debug.Log("Collision with wall while grounded.");
            } else {
                playerRigidBody.useGravity = true;
            }
        }
    }
}