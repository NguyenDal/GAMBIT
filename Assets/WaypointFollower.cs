using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
 
    [SerializeField] float speed = 1.0f;
 
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        transform.LookAt(waypoints[currentWaypointIndex].transform);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}