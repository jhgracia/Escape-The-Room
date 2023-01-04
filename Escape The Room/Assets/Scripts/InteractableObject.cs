using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnInteracted;

    public GameObject cancelTextGO;
    public GameObject interactTextGO;
    public TextMeshProUGUI interactTextTMP;

    [SerializeField] protected string myInteractText = "Interact";

    readonly string commonInteractText = "(E) ";
    [SerializeField] protected bool interacted;
    [SerializeField] bool playerInRange;


    void Start()
    {
        if(interactTextGO.activeSelf) DeactivateInteractText();
        OnStart();
    }

    void Update()
    {
        if (!playerInRange || interacted) return;

        if (MasterManager.main.inputManager.GetInteractValue() > 0)
        {
            ExecuteInteraction();
        }

        OnUpdate();
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    void SwitchInteractText(bool active)
    {
        interactTextGO.SetActive(active);
    }

    protected void ActivateInteractText()
    {
        SwitchInteractText(true);
        interactTextTMP.SetText(commonInteractText + myInteractText);
    }

    protected void DeactivateInteractText()
    {
        interactTextTMP.SetText("");
        SwitchInteractText(false);
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
}
