using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonFrequencies : MonoBehaviour
{
    [SerializeField] GameObject forwardButton;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

    // Start is called before the first frame update
    void Start()
    {
        // Get component from parent (participant object)
        float baselineFreq = gameObject.GetComponentInParent<FrequencyMovement>().GetBaselineFrequency();

        forwardButton.GetComponent<ButtonFlicker>().frequency = baselineFreq + FrequencyMovement.forwardOffset;
        backButton.GetComponent<ButtonFlicker>().frequency = baselineFreq + FrequencyMovement.backwardOffset;
        leftButton.GetComponent<ButtonFlicker>().frequency = baselineFreq + FrequencyMovement.leftOffset;
        rightButton.GetComponent<ButtonFlicker>().frequency = baselineFreq + FrequencyMovement.rightOffset;
    }
}
