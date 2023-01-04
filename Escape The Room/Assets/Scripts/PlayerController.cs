using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3.0f;

    public bool IsInteracting { get; private set; }

    private void FixedUpdate()
    {
        if (MasterManager.main.gameManager.UseMoveAndLook) Move();
    }

    private void Move()
    {
        if (MasterManager.main.inputManager.MoveValue == Vector2.zero) return;

        Vector3 moveDirectionV3 = new Vector3(MasterManager.main.inputManager.MoveValue.x, 0f, MasterManager.main.inputManager.MoveValue.y);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirectionV3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            IsInteracting = true;
            //MasterManager.main.gameManager.ChangeCursorLockState(CursorLockMode.None);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            IsInteracting = false;
//            MasterManager.main.gameManager.ChangeCursorLockState(CursorLockMode.Locked);
        }
            
    }
}
