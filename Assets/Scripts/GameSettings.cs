using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static float movementSpeed = 5f;
    public static float rotationSpeed = 180f;

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("MovementSpeed", movementSpeed);
        PlayerPrefs.SetFloat("RotationSpeed", rotationSpeed);
        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
        movementSpeed = PlayerPrefs.GetFloat("MovementSpeed", movementSpeed);
        rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed", rotationSpeed);
    }
    
}
