using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script flickers the object it is attached to by a specific frequency determined by the participant at the start of the experiment.
 * The object is turned on/off by setting it's scale to 0 on all dimensions, then setting each to default when turning back on. 
 */
public class FlickerScript : MonoBehaviour
{
    public bool flicker;
    private bool isFlickering = false;
    private float flickerFrequency;

    // Start is called before the first frame update
    void Start()
    {
        flickerFrequency = GameObject.FindGameObjectWithTag("Player").GetComponent<FrequencyMovement>().GetBaselineFrequency() + FrequencyMovement.breakWallOffset;
    }
    IEnumerator FlickerObject(float frequency)
    {
        isFlickering = true;

        //The intervals of time (in seconds) should be 1/Frequency (Hz), e.g. if the frequency is 8hz, the object should be OFF for 
        // 1/16th of a second, and ON 1/16th of a second (1/8th second total time.)
        float waitTime = (1 / frequency) / 2;
        while (true)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(waitTime);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(waitTime);
            if(!flicker){
                isFlickering = false;
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(flicker && !isFlickering){
            StartCoroutine(FlickerObject(flickerFrequency));
        }
    }
}
