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

    void start(){
        walls = GameObject.FindGameObjectsWithTag("Cube");
    }
    public void Respawn(){
        participent = GameObject.FindGameObjectWithTag("Player");
        participent.SetActive(false);
        trans = GameObject.FindGameObjectWithTag("Respawn");
        respawnLocation = trans.transform;
        participent.transform.position = respawnLocation.position;
        participent.SetActive(true);
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name.Equals("Participant")){
            Respawn();
        }
    }
    
}
