using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour
{
    //Every object that will be interacted with using the interact key (E) must inherit from this class

    //Use this event to define what will happen after the object has been successfully interacted with and its job is done (e.g. disable the current object and enable another one)
    [SerializeField] protected UnityEvent OnInteracted;

    //Message to be displayed in the Interact Text
    readonly string commonInteractText = "(E) ";
    [SerializeField] protected string myInteractText = "Interact"; //Change this in the inheriting object's inspector

    //Sound clip to be played when the interaction key is pressed
    [SerializeField] AudioClip interactClip;

    //Control variables
    protected bool interacted;
    bool playerInRange;

    protected virtual void Update()
    {
        //Override if the inheriting object needs to run some extra logic

        if (!playerInRange || interacted) return;

        if (MasterManager.Instance.inputManager.IsInteractPerformed())
        {
            //Listen for the Interact key (E)

            PlayAudioClip();
            ExecuteInteraction();
        }
    }

    void PlayAudioClip()
    {
        MasterManager.Instance.audioManager.playPlayerSound(interactClip);
    }

    protected void ActivateInteractText()
    {
        MasterManager.Instance.uiManager.ActivateInteractText(commonInteractText + myInteractText);
    }

    protected void DeactivateInteractText()
    {
        MasterManager.Instance.uiManager.DeactivateInteractText();
    }

    protected void ToggleCancelText(bool active)
    {
        MasterManager.Instance.uiManager.ToggleCancelText(active);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") || interacted) return;

        playerInRange = true;
        ActivateInteractText();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        DeactivateInteractText();
        playerInRange = false;
    }

    protected abstract void ExecuteInteraction();
    //Inheriting objects will use this function to do thier job when interacted
}
