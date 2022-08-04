using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    public TextMeshProUGUI screen;
    public GameObject door;
    KeyButton keyButton;
    bool isDoorOpen;

    private void Start()
    {
        keyButton = GameObject.Find("Key Screen").GetComponent<KeyButton>();
    }

    public void AddNumberToScreen(string number)
    {
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
        if (screen.text == "0" || keyButton.Key == 0 || isDoorOpen) return;

        int localKey;
        if (int.TryParse(screen.text, out localKey))
        {
            if (localKey == keyButton.Key) StartCoroutine(OnOpenDoor());
        }
    }

    IEnumerator OnOpenDoor()
    {
        isDoorOpen = true;

        float step = 0f;
        Quaternion initialRot = door.transform.rotation;
        Quaternion finalRot = Quaternion.Euler(new Vector3(initialRot.x, initialRot.y - 90f, initialRot.z));

        while (step < 1f)
        {
            step += Time.deltaTime;
            door.transform.rotation = Quaternion.Slerp(initialRot, finalRot, step);
            yield return null;
        }
    }
}
