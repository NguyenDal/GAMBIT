using System.Collections;
using UnityEngine;

public class KillBill : MonoBehaviour
{
    public Transform spawnPoint;
    public Animator animator;
    public MonoBehaviour movementScript;
    public float respawnDelay = 1.5f;

    private bool isDead = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (movementScript == null)
            Debug.LogError("Movement script reference is missing!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDead)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        isDead = true;
        DeathManager.IncrementDeathCount();

        if (movementScript != null)
            movementScript.enabled = false;

        animator.Play("Death");
        StartCoroutine(HandleDeathAndRespawn());
    }

    private IEnumerator HandleDeathAndRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        transform.position = spawnPoint.position;

        if (movementScript != null)
            movementScript.enabled = true;

        isDead = false;
    }
}
