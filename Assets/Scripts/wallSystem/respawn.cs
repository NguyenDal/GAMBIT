using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class respawn : MonoBehaviour
{
    private GameObject participent;
    private GameObject trans;
    private Transform respawnLocation;

    void Start(){
        participent = GameObject.FindGameObjectWithTag("Player");
        trans = GameObject.FindGameObjectWithTag("Respawn");
        respawnLocation = trans.transform;
    }

    public void Respawn(GameObject particpent){
        participent.transform.position = respawnLocation.position;
    }
}
