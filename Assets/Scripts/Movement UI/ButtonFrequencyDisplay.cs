using UnityEngine;
using TMPro;

public class ButtonFrequencyDisplay : MonoBehaviour
{
    public TextMeshProUGUI frequencyText; // The text next to the button

    private void Update()
    {
        if (frequencyText != null)
        {
            frequencyText.text = string.Format("{0:0.0} Hz", FrequencyManager.Instance.ButtonFrequency);
        }
    }
}
