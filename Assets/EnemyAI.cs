using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject wayPointA, wayPointB;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update(){
        if(target != null){
            agent.SetDestination(target.position);   
        }
    }
}
