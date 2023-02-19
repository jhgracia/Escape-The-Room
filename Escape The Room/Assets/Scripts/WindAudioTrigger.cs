using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAudioTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        MasterManager.Instance.audioManager.AdjustWindVolume(0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        MasterManager.Instance.audioManager.AdjustWindVolume(1.0f);
    }
}
