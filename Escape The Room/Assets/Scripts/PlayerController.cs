using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] [Range(1f, 20f)] float rotationSpeed = 5.0f;
    [SerializeField] float stepsTime = 0.5f;
    float elapsedTime;
    FootStepsManager footStepsManager;

    private void Awake()
    {
        footStepsManager = GetComponent<FootStepsManager>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (MasterManager.Instance.gameManager.AllowPlayerInput)
        {
            Move();
            Look();
        }
    }

    void Move()
    {
        if (MasterManager.Instance.inputManager.MoveValue == Vector2.zero) return;

        float x = MasterManager.Instance.inputManager.MoveValue.x;
        float z = MasterManager.Instance.inputManager.MoveValue.y;
        Vector3 moveDirection = new Vector3(x, 0f, z);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);

        if (elapsedTime >= stepsTime)
        {
            footStepsManager.PlayStep();
            elapsedTime = 0f;
        }
    }

    void Look()
    {
        if (MasterManager.Instance.inputManager.LookValue.x != 0f)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * MasterManager.Instance.inputManager.LookValue.x);
        }
    }
}
