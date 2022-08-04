using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;

    [SerializeField]  Vector2 m_MoveValue;
    [SerializeField]  Vector2 m_LookValue;

    public Vector2 MoveValue { get { return m_MoveValue.normalized; } }
    public Vector2 LookValue { get { return m_LookValue; } }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
    }

    private void Start()
    {
        m_MoveValue = Vector2.zero;
        m_LookValue = Vector2.zero;
    }

    void Update()
    {
        UpdateMoveValue();
        UpdateLookValue();
    }

    void UpdateMoveValue()
    {
        m_MoveValue = moveAction.ReadValue<Vector2>();
    }

    void UpdateLookValue()
    {
        m_LookValue = lookAction.ReadValue<Vector2>();
    }
}
