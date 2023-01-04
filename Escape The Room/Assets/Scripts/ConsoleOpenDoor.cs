using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleOpenDoor : InteractableObject
{
    public KeyPad keyPad;
    public Door door;
    
    protected override void ExecuteInteraction()
    {
        DeactivateInteractText();
        cancelTextGO.SetActive(true);
        MasterManager.main.gameManager.ChangeCursorLockState(CursorLockMode.None);
        //StartCoroutine(ScaleKeyPad(Vector3.one));
        keyPad.Activate();
    }

    //protected override void OnStart()
    //{
    //    door = keyPad.GetComponent<KeyPad>();
    //}

    protected override void OnUpdate()
    {
        if (!cancelTextGO.activeSelf) return;

        if (MasterManager.main.inputManager.GetCancelValue() > 0)
        {
            CloseKeyPad();
        }
    }

    public void CloseKeyPad()
    {
        cancelTextGO.SetActive(false);
        MasterManager.main.gameManager.ChangeCursorLockState(CursorLockMode.Locked);
        //StartCoroutine(ScaleKeyPad(Vector3.zero));
        keyPad.Deactivate();

        if (keyPad.IsDoorOpenning)
        {
            interacted = true;
            OnInteracted.Invoke();
            return;
        }

        ActivateInteractText();
    }

    //void CheckDoorStatus()
    //{
    //    if (door.IsOpen)
    //    {
    //        interacted = true;
    //        OnInteracted.Invoke();
    //        return;
    //    }

    //    ActivateInteractText();
    //}

    //IEnumerator ScaleKeyPad(Vector3 targetScale)
    //{
    //    float step = 0f;
    //    Vector3 initialScale = keyPad.transform.localScale;

    //    if (initialScale == targetScale) yield break;
        
    //    while (step < 1f)
    //    {
    //        step += Time.deltaTime;
    //        keyPad.transform.localScale = Vector3.Lerp(initialScale, targetScale, step);
    //        yield return null;
    //    }

    //    if (targetScale == Vector3.zero) CheckDoorStatus();
    //}
}
