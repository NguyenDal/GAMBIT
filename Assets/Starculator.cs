using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starculator : MonoBehaviour
{

    private int deader;
    private int cointeres;
    public GameObject star;
    public GameObject another_star;

    // Start is called before the first frame update
    void Start()
    {

        deader = PlayerPrefs.GetInt("Death");
        cointeres = PlayerPrefs.GetInt("CoinCount");


        if (deader <= 3 && cointeres == 4)
        {
            star.SetActive(true);
            another_star.SetActive(true);
        }

        else if(deader <= 5 && cointeres >= 3)
        {
            star.SetActive(true);
            another_star.SetActive(false);
        }

        else
        {
            star.SetActive(false);
            another_star.SetActive(false);
        }

        PlayerPrefs.SetInt("Death", 0);
        PlayerPrefs.SetInt("CoinCount", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
