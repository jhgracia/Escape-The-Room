using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource playerSource;

    public void playPlayerSound(AudioClip clip)
    {
        playerSource.PlayOneShot(clip);
    }
}
