using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeGetter : InteractableObject
{
    //Stores the code given by this object
    public int Key { get; private set; }

    //UI elements
    public TextMeshProUGUI keyText;
    public Image keyScreenImage;
    
    //Control variable
    bool isKeyGiven;

    protected override void ExecuteInteraction()
    {
        if (isKeyGiven) return;

        interacted = true;
        isKeyGiven = true;
        Key = Random.Range(1, 1000000);
        StartCoroutine(GiveKeyRoutine());
        DeactivateInteractText();
    }

    IEnumerator GiveKeyRoutine()
    {
        float targetAlpha = 20f / 255f;
        string keyMessage = "Your code is:\n" + Key;

        MasterManager.Instance.uiManager.FadeImageAlpha(keyScreenImage, targetAlpha, 2f);
        yield return new WaitUntil(() => MasterManager.Instance.uiManager.IsDoneFading);

        MasterManager.Instance.uiManager.DisplayText(keyText, keyMessage, 0.1f);
        yield return new WaitUntil(() => MasterManager.Instance.uiManager.IsDoneWriting);

        OnInteracted.Invoke();
    }
}
