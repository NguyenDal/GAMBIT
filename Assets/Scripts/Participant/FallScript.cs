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
    public float flapStrength;

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
        

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Player on map.");
        //plyr.useGravity = false;

    }

    private void OnCollisionExit(Collision collision)
    {     

        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Player fell off map.");
            plyr.useGravity = true;
        }
       

    }


}
