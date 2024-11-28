using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starculator : MonoBehaviour
{

    private int deader;
    private int cointeres;
    public GameObject star;
    public GameObject another_star;
    private float timerValue;
    public int deathlimit;
    public int coincollectionexpectation;
    public int timerlimit;

    // Start is called before the first frame update
    void Start()
    {

        deader = PlayerPrefs.GetInt("Death");
        cointeres = PlayerPrefs.GetInt("CoinCount");
        timerValue = PlayerPrefs.GetFloat("SavedTime");

        if (deader <= deathlimit && cointeres == coincollectionexpectation && timerValue >= timerlimit)
        {
            star.SetActive(true);
            another_star.SetActive(true);
        }

        else if(deader <= deathlimit + 2 && cointeres >= coincollectionexpectation - 2 && timerValue >= timerlimit - 30)
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
