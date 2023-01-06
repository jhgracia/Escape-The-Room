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
    [SerializeField] ConsoleOpenDoor console;

    [SerializeField] Image enterButton;
    Color colorRed;
    Color colorGreen;
    Color colorYellow;

    public bool IsDoorOpenning { get; private set; }

    private void Awake()
    {
        colorYellow = enterButton.color;
        colorRed = new Color(255f / 255f, 57f / 255f, 49f / 255f);
        colorGreen = new Color(108f / 255f, 253f / 255f, 99f / 255f);
    }

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

        if (screen.text == "0" || codeGetter.Key == 0 || door.IsOpen)
        {
            StartCoroutine(ProcessCode(false));
            return;
        }

        int localKey;
        if (int.TryParse(screen.text, out localKey))
        {
            if (localKey == codeGetter.Key)
            {
                IsDoorOpenning = true;
                StartCoroutine(ProcessCode(true));
                return;
            }
        }

        StartCoroutine(ProcessCode(false));
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

    IEnumerator ProcessCode(bool isCodeCorrect)
    {
        Color tempColor = isCodeCorrect ? colorGreen : colorRed;

        enterButton.color = tempColor;
        yield return new WaitForSeconds(0.5f);
        enterButton.color = colorYellow;

        if (isCodeCorrect)
        {
            door.Open();
            console.CloseKeyPad();
        }
    }
}
