using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class respawn : MonoBehaviour
{
    public GameObject participent;
    public Transform trans;
    
    // if the player enters the collision zone, the player is respawned back to the original position
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")){
            participent.transform.position = trans.position;
            
        }
        
    }
}
