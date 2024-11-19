using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin_Collection_Unit : MonoBehaviour
{
    private int lastSavedCoinCount;
    [SerializeField] private List<GameObject> CoinList = new List<GameObject>();
    public TextMeshProUGUI Coiner;
    private int CoinCount;

    void Start()
    {
        CoinCount = 0;
    }


    void Update()
    {
        CoinCount = 0;
        foreach (var item in CoinList)
        {
            if (!item.activeSelf)
            {
                CoinCount++;
            }
        }

        if (Coiner != null)
        {
            Coiner.text = CoinCount.ToString();
        }


        if (CoinCount != lastSavedCoinCount)
        {
            PlayerPrefs.SetInt("CoinCount", CoinCount);
            lastSavedCoinCount = CoinCount;
        }
    }
}