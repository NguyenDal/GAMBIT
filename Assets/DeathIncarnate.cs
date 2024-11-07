using UnityEngine;

public class DeathIncarnate : MonoBehaviour
{
    public Transform respawnPoint;  
    public float fallThreshold = -10f;  

    void Update()
    {
        
        if (transform.position.y < fallThreshold)
        {
            Debug.Log("dsajdjaks");
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;

        
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }

        Debug.Log("Character respawned at the defined point.");
    }
}
