using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject secondaryCam;
    public bool UseMoveAndLook { get; private set; }

    private void Awake()
    {
        ChangeCursorLockState(CursorLockMode.Locked);
    }

    public void ChangeCursorLockState(CursorLockMode state)
    {
        Cursor.lockState = state;
        Cursor.visible = state == CursorLockMode.None;
        UseMoveAndLook = state != CursorLockMode.None;
    }

    public void SwitchToMainCam()
    {
        if (mainCam.activeSelf) return;

        secondaryCam.SetActive(false);
        mainCam.SetActive(true);
    }

    public void SwitchToSecondaryCam()
    {
        if (secondaryCam.activeSelf) return;

        mainCam.SetActive(false);
        secondaryCam.SetActive(true);
        secondaryCam.transform.localPosition = mainCam.transform.localPosition;
        secondaryCam.transform.localRotation = mainCam.transform.localRotation;
    }

}
