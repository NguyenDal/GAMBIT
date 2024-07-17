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
    AudioSource audioSource;

    bool isNear = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wall = gameObject;
        wallPos =  wall.transform;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        playerPos = player.transform;
        differenceZ = wallPos.position.z - playerPos.position.z;
        differenceX = wallPos.position.x - playerPos.position.x;
        if (wall != null)
        {
            //In order for code to work, make sure walls have box colliders and that player char controller skin width is
            //0.0099. Wall1 should have box coll size of 0.63, and wall2 & 3 should have box coll sizes of 0.7 (for l1).
            //For L2, sizes are 0.85 for walls 1 & 2 and 0.8 for wall3. For l3, wall1 size is 0.95 while walls 2, 3, & 4 0.85. 
            //Above code allows functionality for collisions to occur and also offers impassable wall functionality at low speeds.
            //Wall's nested rigidbody Mass should also be set to a high value - I chose 10000 to ensure functionality.

            //In my environment, values were: level1 wall1 x 0.0001, y 2, z 0.63. wall2, x 0.63, 2, 0.0001. wall3 below
            // wall3 x 0.0001, y 2, z 0.63. Updating other levels shortly. Level2 wall1 is x 0.0001, y 2, z 0.63. Walls2 and 3 have the -
            // Opposite x and z values (i.e. x 0.63, y 2, z 0.0001). Level3 walls 1, 2, and 3 are same as level 2. Wall 4 is -
            //x = 0.63, y = 2, z =  0.1. For some reason this is the lowest the value goes without allowing player to glitch into wall.
            //These values were selected to ensure obj proximity-
            //So as to best simulate someone slamming into a wall. The tradeoff is that this means there will never be collisions.
            //If you for some reason decide to add collisions with the wall that we'd destroy, simply change the values to the those
            //listed in the comment section above this.
            if (differenceX < 1 && differenceX > -1 && differenceZ > -1 && differenceZ < 1)
            {
                if (!isNear)
                {
                    isNear = true;
                    player.GetComponent<InteractionHandler>().AddWall(wall);
                }
                player.GetComponent<FrequencyMovement>().SetIsInblock(false);
            }
            else
            {
                if (isNear)
                {
                    isNear = false;
                    player.GetComponent<InteractionHandler>().RemoveWall(wall);
                }
            }
            

        }
    }

    public void BreakObject()
    {
        StartCoroutine(DestroyAfterAudio());
    }

    private IEnumerator DestroyAfterAudio()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        wall.SetActive(false);
    }
}
