using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorActivationLevel0 : MonoBehaviour
{
    public GameObject connector;

    // Start is called before the first frame update
    void Start()
    {
        connector.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("Player triggered!");

            if (!connector.activeInHierarchy)
            {
                connector.SetActive(true);
            }
        }
    }
    
}
