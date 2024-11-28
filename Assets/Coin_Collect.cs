using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collect : MonoBehaviour
{
    public GameObject coin;
    private AudioSource coin_collect;

    void Start()
    {
        
        coin_collect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("plyrBody"))
        {
            
            if (coin_collect.clip != null)
            {
                AudioSource.PlayClipAtPoint(coin_collect.clip, transform.position);
                Debug.Log("Audio Played");
            }
            else
            {
                Debug.LogError("Audio clip is not assigned!");
            }

           
            coin.SetActive(false);
        }
    }

}
