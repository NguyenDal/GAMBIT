using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class respawn : MonoBehaviour
{
    private GameObject participent;
    private GameObject trans;
    private Transform respawnLocation;

    public void Respawn(){
        participent = GameObject.FindGameObjectWithTag("Player");
        trans = GameObject.FindGameObjectWithTag("Respawn");
        respawnLocation = trans.transform;
        participent.transform.position = respawnLocation.position;
    }
}
