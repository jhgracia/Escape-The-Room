using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] AudioClip goalClip;
    bool isClipTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isClipTriggered) return;

        MasterManager.main.audioManager.playPlayerSound(goalClip);
        isClipTriggered = true;
    }
}
