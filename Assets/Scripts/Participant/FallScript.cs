using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Code currently works as is. This is the best code for functionality and movement without being launched away.
//Note that you will still be pushed off the map when you approach the flashing squares because the collision sphere is
//large. To see what I mean, click on second selector from the right - just above the tab that says hierarchy (i.e. from the
//tab with the hand that you use to drag your way around the screen).

public class FallScript : MonoBehaviour
{

    public Rigidbody plyr;
    

    //below is best solution so far. This is a strange issue that does seem most related to PlayerController script.

    // Start is called before the first frame update
    void Start()
    {
        plyr.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player entering map.");
        plyr.useGravity = false;
        GetComponent<CharacterController>().enabled = true;

        

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Player on map.");
      if (collision.gameObject.tag.Equals("Wall")) { 
        if (GetComponent<CharacterController>().isGrounded)
        {
            GetComponent<CharacterController>().enabled = true;
            Debug.Log("is grnded");
        }
      }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Player fell off map.");
            plyr.useGravity = true;
            plyr.AddForce(Vector3.down * 1000f, ForceMode.Acceleration);
            if (!GetComponent<CharacterController>().isGrounded)
            {
                GetComponent<CharacterController>().enabled = false;
                plyr.useGravity = true;
                plyr.mass = 10000;
                Debug.Log("not grounded");

              //line has been commented out and moved outside of nested if. Appears to more consistently give desired result
              //Remove line below to demonstrate "closest" solution prior to this. Note, 800f appears to be threshold for functionality.
                //plyr.AddForce(Vector3.down * 1000f, ForceMode.Acceleration);
            }


        }
       

    }


}
