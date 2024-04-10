using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tiles2Trigger : MonoBehaviour
{
    //Trigger for each tile
    //if tile is is hit by player, tell this to parent script
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Cylinder"))
        {
            var parentScript = this.GetComponentInParent<LV2tiles>();
            parentScript.giveParentTrigger(this.gameObject);
        }
    }
}
