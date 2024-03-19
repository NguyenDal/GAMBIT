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
            //In order for code to work, make sure walls have box colliders and that player char controller skin width is
            //0.0099. Wall1 should have box coll size of 0.63, and wall2 & 3 should have box coll sizes of 0.7 (for l1).
            //For L2, sizes are 0.85 for walls 1 & 2 and 0.8 for wall3. For l3, wall1 size is 0.95 while walls 2, 3, & 4 0.85. 
            if (differenceX < 0.5 && differenceX > -0.5 && differenceZ > -0.5 && differenceZ < 0.5 && Input.GetKey("p")) {
                wall.SetActive(false);
                GetComponent<CharacterController>().enabled = true;

            }
        }
        
    }
}
