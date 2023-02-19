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

        MasterManager.Instance.audioManager.playPlayerSound(goalClip);
        isClipTriggered = true;
        Invoke("ChangeGameStatus", 3f);
    }

    void ChangeGameStatus()
    {
        MasterManager.Instance.gameManager.CurrentGameStatus = GameManager.GameStatus.levelEnding;
    }
}
