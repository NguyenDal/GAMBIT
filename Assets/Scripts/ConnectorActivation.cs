using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorActivation : MonoBehaviour
{
    public GameObject connector1;
    public GameObject connector2;
    public GameObject connector3;
    public GameObject connector4;

    void Start()
    {
        connector1.SetActive(false);
        connector2.SetActive(false);
        connector3.SetActive(false);

        if (connector4 != null) 
        {
            connector4.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("Player triggered!");

            if (!connector1.activeInHierarchy)
            {
                connector1.SetActive(true);
            }
            else if (!connector2.activeInHierarchy)
            {
                connector2.SetActive(true);
            }
            else if (!connector3.activeInHierarchy)
            {
                connector3.SetActive(true);
            }
            else if (connector4 != null && !connector4.activeInHierarchy)
            {
                connector4.SetActive(true);
            }
        }
    }
}