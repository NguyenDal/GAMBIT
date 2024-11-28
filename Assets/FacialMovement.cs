using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OpenSee.OpenSee;

using DS = data.DataSingleton;

public class FacialMovement : MonoBehaviour
{
    TrackingAPI TrackingAPI;
    GameObject openSee;
    FrequencyMovement freqMovement;

    private static int rightThreshold = 30;
    private static int leftThreshold = -rightThreshold;
    private static float mouthThreshold = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        freqMovement = gameObject.GetComponent<FrequencyMovement>();
        // only enable Facial movement if the user selected it on the title screen.
        if (PlayerPrefs.GetInt("FacialMovement" + "Enabled") == 1)
        {
            Debug.Log("Enabling Facial Controller...");
            // Get the OpenSee singleton
            openSee = GameObject.Find("OpenSee");


            // Retrieve the TrackingAPI module from the singleton
            TrackingAPI = openSee.GetComponent<TrackingAPI>();

            // bind our functions to the iterated list
            TrackingAPI.bindToList(LookRightCheck, LookRight);
            TrackingAPI.bindToList(LookLeftCheck, LookLeft);
            TrackingAPI.bindToList(OpenMouthCheck, MoveForward);
        }
        
    }

    bool LookRightCheck(OpenSeeData data)
    {
        if (data.rotation.y > rightThreshold)
        {
            return true;
        }

        return false;
    }

    void LookRight(OpenSeeData data)
    {
        freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.rightOffset);
    }


    bool LookLeftCheck(OpenSeeData data)
    {
        if (data.rotation.y < leftThreshold)
        {
            return true;
        }

        return false;
    }

    void LookLeft(OpenSeeData data)
    {
        freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.leftOffset);
    }

    bool OpenMouthCheck(OpenSeeData data)
    {
        if (data.features.MouthWide > mouthThreshold)
        {
            return true;
        }
        return false;
    }

    void MoveForward(OpenSeeData data)
    {
        freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.forwardOffset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
