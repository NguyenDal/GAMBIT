using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code currently works as is. This is the best code for functionality and movement without being launched away.
//Note that you will still be pushed off the map when you approach the flashing squares because the collision sphere is
//large. To see what I mean, click on second selector from the right - just above the tab that says hierarchy (i.e. from the
//tab with the hand that you use to drag your way around the screen).

public class FallScript : MonoBehaviour
{

    //public GameObject n;
    public Rigidbody plyr;
    public float flapStrength;

    // Start is called before the first frame update
    void Start()
    {
        plyr.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            plyr.velocity = Vector3.up * flapStrength;    
        }*/

    }
    

    private void OnCollisionEnter(Collision collision)
    {
        //plyr.isKinematic = true;
        Debug.Log("Player entering map.");
        plyr.useGravity = false;
        

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Player on map.");
        //plyr.isKinematic = true;
        //plyr.useGravity = false;

    }

    private void OnCollisionExit(Collision collision)
    {     

        if (collision.gameObject.tag == "Wall")
        {
            //plyr.isKinematic = false;
            Debug.Log("Player fell off map.");
            plyr.useGravity = true;
        }
        /*
         * Original code
         * 
         * if (collision.gameObject.tag == "Wall" && transform.position.y < 0.184)
        {
            Debug.Log("Player fell off map.");
        }*/

        /* Above is more specific - when clicking play button at top of unity cylinder falls over. Above code (rightly) 
         * does not consider that to mean the player fell off the map. Below code, however, would print "player fell off map" 
         * in console. Number i.e. 0.184 is derived from y position listed under transform under cylinder. 
         * 
         * if(collision.gameObject.tag == "Wall" && transform.position.y < 0.184)
        {
            Debug.Log("Player fell off map.");
        }*/

    }


}
