using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed = 5f;     
    private int currentWaypointIndex = 0; 

    void Update()
    {
       
        if (waypoints.Length == 0) return;

        
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;

       
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        }
    }

    private void OnDrawGizmos()
    {
       
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (i + 1 < waypoints.Length)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            else
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position); 
        }
    }
}
