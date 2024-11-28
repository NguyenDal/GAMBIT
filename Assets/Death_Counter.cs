using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Death_Counter : MonoBehaviour
{

    public TextMeshProUGUI Deather;
    private int DeathCount;

    // Start is called before the first frame update
    void Start()
    {
        DeathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCount = PlayerPrefs.GetInt("Death");
        Deather.text = DeathCount.ToString();


    }

}
