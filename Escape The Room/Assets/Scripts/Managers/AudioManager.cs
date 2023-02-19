using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource playerSource, windSource;

    public void GetReferences()
    {
        //Debug.Log($"getting {this.ToString()} references");
        playerSource = GameObject.Find("Player").GetComponent<AudioSource>();
        windSource = GameObject.Find("Wind").GetComponent<AudioSource>();
        if (playerSource == null) Debug.Log("WARNING! playerSource is null");
        if (windSource == null) Debug.Log("WARNING! windSource is null");
    }

    public void playPlayerSound(AudioClip clip)
    {
        playerSource.PlayOneShot(clip);
    }

    public void StartPlayingWind()
    {
        windSource.Play();
    }

    public void AdjustWindVolume(float volume)
    {
        windSource.volume = volume;
    }

    public void AdjustMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
