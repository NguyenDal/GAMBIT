using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public AudioSource LevelFinish;

    public AudioManager(AudioSource source)
    {
        LevelFinish = source;
    }

    public void PlayAudio()
    {
        if (LevelFinish != null)
        {
            LevelFinish.Play();
        }
        else
        {
            Debug.LogError("AudioSource is not set.");
        }
    }
}
