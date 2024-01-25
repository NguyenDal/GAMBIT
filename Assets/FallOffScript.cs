using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffScript : MonoBehaviour
{

    public GameObject n;
    public Rigidbody plyr;
    public float flapStrength;
   
    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true )
        {
            plyr.velocity = Vector3.up * flapStrength;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && transform.position.y < 0.184)
        {
            Debug.Log("Player fell off map.");
        }

        /* Above is more specific - when clicking play button at top of unity cylinder falls over. Above code (rightly) 
         * does not consider that to mean the player fell off the map. Below code, however, would print "player fell off map" 
         * in console. Number i.e. 0.184 is derived from y position listed under transform under cylinder. 
         * 
         * if(collision.gameObject.tag == "Wall" && transform.position.y < 0.184)
        {
            Debug.Log("Player fell off map.");
        }*/

    }

    //currently producing code "player fell off map" in console. Similarly, cylinder now falls if not on floor -
    //hard to test as the cylinder starts off map by default, but i added space bar functionality which allowed me to launch
    // the cylinder back intoo screen and onto the map. It would stay put if it landed on top of a tile (i.e. it did not --
    // fall through the tile). Suggests its ready? Could probably remove spacebar functionality and/or replace with a better -
    //testing system. Otherwise, appears functional. Had idea to implement a flying testing tool. Currently spacebar launches
    // cylinder across the screen. If we change code to be like a jetpack instead, it would be much better for testing.


}
