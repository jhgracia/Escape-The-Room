using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : SecondaryCamCaller
{
    [Space]
    [Space]
    [SerializeField] TextMeshProUGUI screen;
    [SerializeField] Door door;
    [SerializeField] CodeGetter codeGetter;

    public bool IsDoorOpenning { get; private set; }

    public void AddNumberToScreen(string number)
    {
        //Called by the keypad's numerical buttons to enter the numbers in the text field acting as the screen

        if (screen.text.Length >= 6) return;

        if (screen.text == "0")
        {
            screen.text = number;
            return;
        }

        screen.text += number;
    }

    public void OpenDoor()
    {
        //Called by the keypad's enter button to try and open the door

        if (screen.text == "0" || codeGetter.Key == 0 || door.IsOpen) return;

        int localKey;
        if (int.TryParse(screen.text, out localKey))
        {
            if (localKey == codeGetter.Key)
            {
                IsDoorOpenning = true;
                door.Open(); 
            }
        }
    }

    public void Activate()
    {
        //Scales the keypad up so it becomes visible and calls the secondary camera to face the keypad

        StartCoroutine(ScaleKeyPad(Vector3.one));
        CallCamera();
    }

    public void Deactivate()
    {
        //Scales the keypad down so it becomes invisible and resets the secondary camera if the door was not opened

        StartCoroutine(ScaleKeyPad(Vector3.zero));
        if (!IsDoorOpenning) ResetCamera();
    }

    IEnumerator ScaleKeyPad(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale;

        if (initialScale == targetScale) yield break;

        float step = 0f;
        while (step < 1.0f)
        {
            step += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, step);
            yield return null;
        }
    }
}
