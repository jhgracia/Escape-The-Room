using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyButton : MonoBehaviour
{
    public int Key { get; private set; }
    public TextMeshProUGUI keyText;
    
    bool isKeyGiven;

    public void GetKey()
    {
        if (isKeyGiven) return;

        Key = Random.Range(1, 1000000);
        StartCoroutine(GiveKey());
        isKeyGiven = true;
    }

    IEnumerator GiveKey()
    {
        Image image = GetComponent<Image>();
        Color screenColor = image.color;
        float originalAlpha = screenColor.a;
        float step = 0f;
        float targetAlpha = 140f / 255f;
        string keyMessage = "Your key is:\n" + Key;
        string currentText = "";
        
        while (step < 1f)
        {
            step += Time.deltaTime;
            screenColor.a = Mathf.Lerp(originalAlpha, targetAlpha, step);
            image.color = screenColor;
            yield return null;
        }
        
        for (int i = 0; i < keyMessage.Length; i++)
        {
            currentText = keyMessage.Substring(0, i + 1);
            keyText.SetText(currentText);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
