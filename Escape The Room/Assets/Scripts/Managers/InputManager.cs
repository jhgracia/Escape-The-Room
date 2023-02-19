using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction interactAction;
    InputAction cancelAction;

    Vector2 m_MoveValue;
    Vector2 m_LookValue;
    bool m_IsCancelPerformed;

    public Vector2 MoveValue { get { return m_MoveValue.normalized; } }
    public Vector2 LookValue { get { return m_LookValue; } }
    public bool IsCancelPerformed { get { return m_IsCancelPerformed; } }

    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        interactAction = playerInput.actions["Interact"];
        cancelAction = playerInput.actions["Cancel"];
    }

    void Update()
    {
        UpdateMoveValue();
        UpdateLookValue();
        UpdateCancelValue();
    }

    void UpdateMoveValue()
    {
        m_MoveValue = moveAction.ReadValue<Vector2>();
    }

    void UpdateLookValue()
    {
        m_LookValue = lookAction.ReadValue<Vector2>();
    }

    void UpdateCancelValue()
    {
        // This is used to pause/unpause the game and to cancel interaction with objects
        m_IsCancelPerformed = cancelAction.WasPerformedThisFrame();
    }

    public bool IsInteractPerformed()
    {
        //This is validated by interactable objects only when the player is within interaction range, so there's no need to keep an updated variable every frame
        return interactAction.WasPerformedThisFrame();
    }
}
