using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    private List<GameObject> closeWalls = new List<GameObject>();
    
    public void BreakWall()
    {
        if (closeWalls.Count > 0)
        {
            GameObject wall = closeWalls[0];
            closeWalls.RemoveAt(0);
            wall.GetComponent<DestroyObjectScript>().BreakObject();
        }
    }

    public void AddWall(GameObject wall)
    {
        if (wall.GetComponent<DestroyObjectScript>() != null)
        {
            closeWalls.Add(wall);
        }
    }

    public void RemoveWall(GameObject wall)
    {
        closeWalls.Remove(wall);
    }
}
