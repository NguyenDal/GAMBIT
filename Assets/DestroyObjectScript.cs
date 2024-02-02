using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    public GameObject wall;    //gameobj1
    public GameObject player;   //gameobj2
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (wall != null) {
            if (Input.GetKey(KeyCode.UpArrow) && (player.transform.position.x <= wall.transform.position.x + 1) && 
            (player.transform.position.x >= wall.transform.position.x - 1) && (player.transform.position.y <= wall.transform.position.y + 1) &&
            (player.transform.position.y >= wall.transform.position.y - 1)) {
                wall.SetActive(false);
            }
        }
    }
    // void OnTriggerEnter(Collider other) {
    //     Debug.Log("Collided!");
    //     if (other.gameObject.CompareTag("Wall")) {
            
    //     }  
    // }
}