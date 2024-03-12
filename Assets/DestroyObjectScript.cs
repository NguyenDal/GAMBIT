using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    private GameObject wall;    //gameobj1
    private GameObject player;   //gameobj2
    
    private Transform playerPos;
    private Transform wallPos;
    private double differenceX;
    private double differenceZ;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wall = gameObject;
        wallPos =  wall.transform;
    }

    // Update is called once per frame
    void Update() {
        playerPos = player.transform;
        differenceZ = wallPos.position.z - playerPos.position.z;
        differenceX = wallPos.position.x - playerPos.position.x;
        if (wall != null) {
            Debug.Log("destroy: true");
            if (differenceX < 1 && differenceX > -1 && differenceZ > -1 && differenceZ < 1) {
                
                wall.SetActive(false);
            }
        }
    }
}
