using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FrequencyMovement : MonoBehaviour
{
    public bool frequencyMovementEnabled = false;

    [SerializeField] Animator charaterAnimator;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    public const float forwardOffset = 0;
    public const float backwardOffset = 1;
    public const float leftOffset = 2;
    public const float rightOffset = 3;
    public const float breakWallOffset = 4;

    [SerializeField] float tileSize = 0.5f;
    [SerializeField] float turnAngle = 45f;
    [SerializeField] float turnTime = 0.3f;

    private bool isMoving = false;
    private bool isTurning = false;
    public bool isInblock = false;
    private Vector3 lastLocation;

    

    // Default movement speeds (will be overwritten by PlayerPrefs)
    [SerializeField] float defaultMovementSpeed = 4f;
    [SerializeField] float defaultBaselineFrequency = 10;

    void Start()
    {
        // If frequency movement is not enabled, this script is disabled!
        if (frequencyMovementEnabled)
        {
            // Default hertz text
            text.SetText("Previous Frequency: --");

            // Retrieve movement speed from PlayerPrefs, default to defaultMovementSpeed if not found
            float movementSpeed = PlayerPrefs.GetFloat("MovementSpeed", defaultMovementSpeed);

            if (movementSpeed > 4)
                movementSpeed = 4f;

            // Apply retrieved speeds
            SetMovementSpeed(movementSpeed);
            turnAngle = (float)PlayerPrefs.GetInt("PlayerAngle", (int)turnAngle);

        }
        else
        {
            OnDisable();
        }
    }

    // Resets values on disable/die
    private void OnDisable()
    {
        isMoving = false;
        charaterAnimator.SetBool("IsWalking", isMoving);
        isTurning = false;
        text.SetText("");
    }

    // Update input based on frequency
    public void UpdateFrequency(float frequency)
    {
        float baselineFreq = GetBaselineFrequency();

        // Update last frequency received text
        if (text != null)
        {
        text.SetText(string.Format("Previous Frequency: {0:0}", frequency));
        }

        if (IsGrounded() && !isMoving && !isTurning)
        {
            switch (frequency - baselineFreq)
            {
                // Forward
                case forwardOffset:
                    if (isInblock)
                    {
                        break;
                    }
                    lastLocation = transform.position;
                    Debug.Log("LASTPOS F: " + lastLocation);
                    isMoving = true;
                    charaterAnimator.SetBool("IsWalking", isMoving);
                    StartCoroutine(MoveDirection(tileSize, transform.forward));
                    break;

                // Backward
                case backwardOffset:
                    if (isInblock)
                    {
                        break;
                    }
                    lastLocation = transform.position;
                    Debug.Log("LASTPOS B: " + lastLocation);
                    isMoving = true;
                    charaterAnimator.SetBool("IsWalking", isMoving);
                    StartCoroutine(MoveDirection(tileSize, -transform.forward));
                    break;
                
                // Left
                case leftOffset:
                    if (isInblock)
                    {
                        break;
                    }
                    isTurning = true;
                    StartCoroutine(TurnDirection(-turnAngle, turnTime));
                    break;
                
                // Right
                case rightOffset:
                    if (isInblock)
                    {
                        break;
                    }
                    isTurning = true;
                    StartCoroutine(TurnDirection(turnAngle, turnTime));
                    break;

                // Break wall / interact
                case breakWallOffset:
                    gameObject.GetComponent<InteractionHandler>().BreakWall();
                    break;

                default: 
                    break;
            }
        }
    }

    // Move in a direction a certain distance at specified movement speed
    private IEnumerator MoveDirection(float distance, Vector3 direction)
    {
        // Checks if within 5 degrees of 90, if not, move as if diagonal
        bool withinRangeOf90 = (gameObject.transform.eulerAngles.y + 5) % 90 <= 10;

        // If moving diagonally, move a bit farther to end in the center of tile. Moves at same speed.
        float moveDistance = withinRangeOf90 ? distance : Mathf.Sqrt(2) * distance;

        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (direction * moveDistance);

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        float progress = 0;

        // Move until given distance has been crossed
        while (progress < moveDistance && !isInblock)
        {
            // Lerp while grounded, else be affected by gravity (keeps player on tile grid)
            if (IsGrounded())
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, progress / moveDistance);
            }
            else
            {
                rb.rotation = gameObject.transform.rotation;
                rb.velocity = new Vector3(0, rb.velocity.y, 0) + direction * GetMovementSpeed();
            }

            // If progress is larger than distance, don't over shoot distance
            progress += GetMovementSpeed() * Time.deltaTime;
            progress = progress > moveDistance ? moveDistance : progress;

            yield return null;
        }

        if (IsGrounded() && !isInblock )
        {
            transform.position = finalPos;
        }

        isMoving = false;
        charaterAnimator.SetBool("IsWalking", isMoving);
    }

    // Turn an amount of degrees in a specified amount of time
    private IEnumerator TurnDirection(float angle, float rotationTime)
    {
        Quaternion startingRotation = transform.rotation;
        Quaternion finalRotation = startingRotation * Quaternion.Euler(0, angle, 0);

        float elapsedTime = 0;

        while (elapsedTime < rotationTime)
        {
            // Calculate interpolation factor based on elapsed time
            float t = elapsedTime / rotationTime;

            // Interpolate rotation
            transform.rotation = Quaternion.Slerp(startingRotation, finalRotation, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure final rotation is exact
        transform.rotation = finalRotation;
        isTurning = false;
    }

    // Method to set movement speed
    private void SetMovementSpeed(float speed)
    {
        PlayerPrefs.SetFloat("MovementSpeed", speed);
        PlayerPrefs.Save();
    }

    // Method to get movement speed
    private float GetMovementSpeed()
    {
        return PlayerPrefs.GetFloat("MovementSpeed", defaultMovementSpeed);
    }

    // Method to set baseline frequency
    public void SetBaselineFrequency(float frequency)
    {
        PlayerPrefs.SetFloat("BaselineFrequency", frequency);
        PlayerPrefs.Save();
    }

    // Method to get baseline frequency
    public float GetBaselineFrequency()
    {
        return PlayerPrefs.GetFloat("BaselineFrequency", defaultBaselineFrequency);
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        // The !isInBlock is there to make sure we dont change the last location after it is changed once. This is to prevent
        // The player from being stuck in the block by having the lastLocation being inside the block. 
        if (other.gameObject.tag.Equals("Cube") && !isInblock)
        {
            Debug.Log("IN BLOCK");
            isInblock = true;
            transform.position = lastLocation;
        }
    }

    public void SetIsInblock(bool value)
    {
        this.isInblock = value;
    }
}
