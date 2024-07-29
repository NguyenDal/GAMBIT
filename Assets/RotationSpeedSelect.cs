using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotationSpeedSelect : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private int DROPDOWN45 = 0;

    private int DROPDOWN90 = 1;
    // Start is called before the first frame update
    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dropdown.value == DROPDOWN45)
        {
            Debug.Log("Dropdown: 45");
            // Set value to 45.
            
        } else if (_dropdown.value == DROPDOWN90)
        {
            // Set value to 90
            Debug.Log("Dropdown: 90");
        }
    }
}
