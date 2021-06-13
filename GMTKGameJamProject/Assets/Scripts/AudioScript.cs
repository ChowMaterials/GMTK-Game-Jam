using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource MainMusic;
    
    void Start()
    {
        MainMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    
}
