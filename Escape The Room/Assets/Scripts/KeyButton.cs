using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyButton : InteractableObject
{
    public int Key { get; private set; }
    public TextMeshProUGUI keyText;
    public Image keyScreenImage;
    
    bool isKeyGiven;

    protected override void ExecuteInteraction()
    {
        if (isKeyGiven) return;

        interacted = true;
        isKeyGiven = true;
        Key = Random.Range(1, 1000000);
        StartCoroutine(GiveKey());
        DeactivateInteractText();
    }

    IEnumerator GiveKey()
    {
        Color screenColor = keyScreenImage.color;
        float originalAlpha = screenColor.a;
        float step = 0f;
        float targetAlpha = 25f / 255f;
        string keyMessage = "Your key is:\n" + Key;
        string currentText;
        
        while (step < 1f)
        {
            step += Time.deltaTime / 2;
            screenColor.a = Mathf.Lerp(originalAlpha, targetAlpha, step);
            keyScreenImage.color = screenColor;
            yield return null;
        }
        
        for (int i = 0; i < keyMessage.Length; i++)
        {
            currentText = keyMessage.Substring(0, i + 1);
            keyText.SetText(currentText);
            yield return new WaitForSeconds(0.1f);
        }

        OnInteracted.Invoke();
    }
}
