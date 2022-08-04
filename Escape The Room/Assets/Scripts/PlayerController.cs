using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    GameManager gameManager;

    [SerializeField] float moveSpeed = 3.0f;

    public bool IsInteracting { get; private set; }

    void Start()
    {
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (inputManager.MoveValue == Vector2.zero) return;

        Vector3 moveDirectionV3 = new Vector3(inputManager.MoveValue.x, 0f, inputManager.MoveValue.y);
        transform.Translate(moveSpeed * Time.deltaTime * moveDirectionV3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            IsInteracting = true;
            gameManager.ChangeCursorLockState(CursorLockMode.None);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            IsInteracting = false;
            gameManager.ChangeCursorLockState(CursorLockMode.Locked);
        }
            
    }
}
