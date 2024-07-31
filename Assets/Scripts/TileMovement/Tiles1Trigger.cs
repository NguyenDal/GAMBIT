using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiles1Trigger : MonoBehaviour
{
    //Trigger for each tile
    //if tile is is hit by player, tell this to parent script
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("plyrBody"))
        {
            var parentScript = this.GetComponentInParent<LV1tiles>();
            parentScript.giveParentTrigger(this.gameObject);            
        }
    }
}