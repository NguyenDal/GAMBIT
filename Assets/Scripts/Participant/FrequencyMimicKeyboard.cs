using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrequencyMimicKeyboard : MonoBehaviour
{
    FrequencyMovement freqMovement = null;

    public Animator charaterAnimator;
    
    private void Start()
    {
        freqMovement = gameObject.GetComponent<FrequencyMovement>();
        
        // If frequency movement is not enabled, disable this script.
        if (!(freqMovement != null && freqMovement.frequencyMovementEnabled))
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = false;

        // Forward
        if (Keyboard.current.wKey.isPressed)
        {
            freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.forwardOffset);
            isWalking = true;
        }

        // Backward
        if (Keyboard.current.sKey.isPressed)
        {
            freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.backwardOffset);
            isWalking = true;
        }

        // Left
        if (Keyboard.current.aKey.isPressed)
        {
            freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.leftOffset);
            
        }

        // Right
        if (Keyboard.current.dKey.isPressed)
        {
            freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.rightOffset);
          
        }

        // Interact
        if (Keyboard.current.pKey.isPressed)
        {
            freqMovement.UpdateFrequency(freqMovement.GetBaselineFrequency() + FrequencyMovement.breakWallOffset);
        
        }

        // Update the animator based on whether the character is walking
        charaterAnimator.SetBool("IsWalking", isWalking);

    }
}
