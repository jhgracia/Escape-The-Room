using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        ChangeCursorLockState(CursorLockMode.Locked);
    }

    public void ChangeCursorLockState(CursorLockMode state)
    {
        Cursor.lockState = state;
        Cursor.visible = state == CursorLockMode.None;
    }
}
