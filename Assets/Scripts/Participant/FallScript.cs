using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Code currently works as is. This is the best code for functionality and movement without being launched away.
//Note that you will still be pushed off the map when you approach the flashing squares because the collision sphere is
//large. To see what I mean, click on second selector from the right - just above the tab that says hierarchy (i.e. from the
//tab with the hand that you use to drag your way around the screen).


//below is working except in level 2, in level 2 it breaks completely CHECK InputManager script (input/manager.cs), it seems to
//be source of our issues

public class FallScript : MonoBehaviour
{

    public Rigidbody plyr;


    //below is best solution so far. This is a strange issue that does seem most related to PlayerController script.

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterController>().enabled = true;

        //plyr.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player entering map.");
        //plyr.useGravity = false;
        GetComponent<CharacterController>().enabled = true;




    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Player on map.");
        GetComponent<CharacterController>().enabled = true;
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            if (GetComponent<CharacterController>().enabled == false)
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    Debug.Log("Player still on map, but unable to move");
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        /*if (collision.gameObject.tag.Equals("Wall"))
        {
            if (GetComponent<CharacterController>().isGrounded)
            {
                GetComponent<CharacterController>().enabled = true;
                Debug.Log("is grnded");
            }
        }*/

    }

    private void OnCollisionExit(Collision collision)
    {

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            Debug.Log("key held.");
            GetComponent<CharacterController>().enabled = false;

        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            Debug.Log("key let go.");
            if (GetComponent<CharacterController>().enabled == false)
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    Debug.Log("key let go and groundedd .");
                    GetComponent<CharacterController>().enabled = true;
                }
            }
        }

        /*
         * Useful piece of code that was the last thing I commented out. I think this is actually a great addition, but left it -
         * out for now so that its clear whats responsible for the bug.
         *
         * if (!GetComponent<CharacterController>().isGrounded && GetComponent<CharacterController>().enabled == true)
        {
            GetComponent<CharacterController>().enabled = true;
 
        }*/

        //another potential solution, remove below commented out code to return to version in git lab. This solution works, but
        // character does a weird jump when it walks off the map - this can be exploited to land on

        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Player fell off map.");
            if (GetComponent<CharacterController>().isGrounded)
            {
                Debug.Log("!- nested it is grounded");

            }
            //plyr.useGravity = true;
            ///plyr.AddForce(Vector3.down * 1100f, ForceMode.Acceleration);
            else if (!GetComponent<CharacterController>().isGrounded)
            {
                //GetComponent<CharacterController>().enabled = false;
                plyr.useGravity = true;
                //plyr.AddForce(Vector3.down * 1100f, ForceMode.Acceleration);
                //plyr.mass = 10000;
                Debug.Log("not grounded");

                //line has been commented out and moved outside of nested if. Appears to more consistently give desired result
                //Remove line below to demonstrate "closest" solution prior to this. Note, 800f appears to be threshold for functionality.
                //plyr.AddForce(Vector3.down * 1000f, ForceMode.Acceleration);
            }
            /*if (GetComponent<CharacterController>().isGrounded){
                Debug.Log("psyke it is grounded");
            }*/


        }

        //adding below line of code has helped - it isolated the issue as explicitly occurring when the user is dealing with the
        //flickering square. This suggests, to me, that the flickering square interaction is the issue. I actually managed to -
        //complete the level, but each time I interacted with the ^ square it glitched, causing the player to fall through or off -
        //the map. I could then retrace my steps and pick up the coloured square and continue to the next flickering box.
        //Thus isolating the issue.
        else if (collision.gameObject.tag != "Wall")
        {
            return;
        }


    }



}