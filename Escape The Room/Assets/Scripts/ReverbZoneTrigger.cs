using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneTrigger : MonoBehaviour
{
    [SerializeField] GameObject audioReverbZone;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        audioReverbZone.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        audioReverbZone.SetActive(false);
    }
}
