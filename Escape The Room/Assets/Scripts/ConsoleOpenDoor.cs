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
        ToggleCancelText(true);
        MasterManager.Instance.gameManager.CurrentGameStatus = GameManager.GameStatus.interact;
        keyPad.Activate();
    }

    protected override void Update()
    {
        base.Update();

        if (MasterManager.Instance.gameManager.CurrentGameStatus != GameManager.GameStatus.interact) return;

        if (MasterManager.Instance.inputManager.IsCancelPerformed)
        {
            //Listen for the Cancel key (Esc)
            CloseKeyPad();
        }
    }

    public void CloseKeyPad()
    {
        ToggleCancelText(false);
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
