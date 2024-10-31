using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentNumberGenerator : MonoBehaviour
{
    public static int launchCount;
    // Start is called before the first frame update
    void Start()
    {
        launchCount = PlayerPrefs.GetInt("TimesLaunched", 0);
        launchCount++;
        PlayerPrefs.SetInt("TimesLaunched", launchCount);
        gameObject.GetComponent<Text>().text += launchCount;
        Destroy(this);
    }
}
