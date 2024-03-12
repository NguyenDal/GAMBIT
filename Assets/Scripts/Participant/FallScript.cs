using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Code currently works as is. This is the best code for functionality and movement.



public class FallScript : MonoBehaviour
{

    public Rigidbody plyr;


    //below is best solution so far. This is a strange issue that does seem most related to PlayerController script.

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterController>().enabled = true;


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
        // character does a weird jump when it walks off the map - this can be exploited to add a falling animation however

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
                Debug.Log("not grounded");

                //line has been commented out and moved outside of nested if. Appears to more consistently give desired result
                //Remove line below to demonstrate "closest" solution prior to this. Note, 800f appears to be threshold for functionality.
                //plyr.AddForce(Vector3.down * 1000f, ForceMode.Acceleration);
            }
            /*if (GetComponent<CharacterController>().isGrounded){
                Debug.Log("it is grounded");
            }*/


        }

        else if (collision.gameObject.tag != "Wall")
        {
            return;
        }


    }



}