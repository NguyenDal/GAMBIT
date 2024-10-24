using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;  //volume slider reference in scene

    void Start()
    {



        //equalize slider value to the audio listener class
        //audio listener is tied to the main player, it represents ears

        //adds volume to playerprefs
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);

        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        Debug.Log("Audio listener test" + volumeSlider.value);



        //add listener to check for changes made to slider
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
    }


    //helper function for setting volume
    public void SetGlobalVolume(float sliderValue)
    {

        AudioListener.volume = sliderValue;
        PlayerPrefs.SetFloat("Volume", sliderValue);
        PlayerPrefs.Save();

    }
}
