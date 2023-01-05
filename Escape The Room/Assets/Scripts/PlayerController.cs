using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] [Range(1f, 20f)] float rotationSpeed = 5.0f;

    private void FixedUpdate()
    {
        if (MasterManager.main.gameManager.UseMoveAndLook)
        {
            Move();
            Look();
        }
    }

    void Move()
    {
        if (MasterManager.main.inputManager.MoveValue == Vector2.zero) return;

        float x = MasterManager.main.inputManager.MoveValue.x;
        float z = MasterManager.main.inputManager.MoveValue.y;
        Vector3 moveDirection = new Vector3(x, 0f, z);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
    }

    void Look()
    {
        if (MasterManager.main.inputManager.LookValue.x != 0f)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * MasterManager.main.inputManager.LookValue.x);
        }
    }
}
