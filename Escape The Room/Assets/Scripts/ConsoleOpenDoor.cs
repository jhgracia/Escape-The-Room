using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleOpenDoor : InteractableObject
{
    //Game objects' scripts needed for this object
    public KeyPad keyPad;
    public Door door;
    
    protected override void ExecuteInteraction()
    {
        DeactivateInteractText();
        cancelTextGO.SetActive(true);
        MasterManager.main.gameManager.ChangeCursorLockState(CursorLockMode.None);
        keyPad.Activate();
    }

    protected override void Update()
    {
        base.Update();

        if (!cancelTextGO.activeSelf) return;

        if (MasterManager.main.inputManager.GetCancelValue() > 0)
        {
            //Listen for the Cancel key (Esc)
            CloseKeyPad();
        }
    }

    public void CloseKeyPad()
    {
        cancelTextGO.SetActive(false);
        MasterManager.main.gameManager.ChangeCursorLockState(CursorLockMode.Locked);
        keyPad.Deactivate();

        if (keyPad.IsDoorOpenning)
        {
            //If the door was successfully opened from the keypad, this object's job is done

            interacted = true;
            OnInteracted.Invoke();
            return;
        }

        ActivateInteractText();
    }
}
