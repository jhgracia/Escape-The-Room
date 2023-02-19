using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    //UI elements
    GameObject cancelTextGO;
    GameObject interactTextGO;
    GameObject settingsImageGO;
    GameObject startButton, restartButton, continueButton, quitButton;
    TextMeshProUGUI interactTextTMP;
    Image messagesImage;
    Slider volumeSlider;

    //UI Messages
    [SerializeField] [Multiline] string startMessage;
    [SerializeField] [Multiline] string endMessage;
    [SerializeField] [Range(0.01f, 0.5f)] float startEndMessageDelay = 0.08f;
    [SerializeField] [Range(1f, 5f)] float startEndFadingTime = 3f;

    //Control properties
    public bool IsDoneWriting { get; private set; }
    public bool IsDoneFading { get; private set; }

    // Initialization functions...
    public void GetReferences()
    {
        // Get the references to the UI game objects

        //Debug.Log($"getting {this.ToString()} references");
        messagesImage = GameObject.Find("Messages Image").GetComponent<Image>();
        cancelTextGO = GameObject.Find("Cancel Text");
        interactTextGO = GameObject.Find("Interact Text");
        interactTextTMP = interactTextGO.GetComponent<TextMeshProUGUI>();
        startButton = GameObject.Find("Start Button");
        restartButton = GameObject.Find("Restart Button");
        continueButton = GameObject.Find("Continue Button");
        quitButton = GameObject.Find("Quit Button");
        settingsImageGO = GameObject.Find("Settings Image");
        volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();

        // Set the buttons listeners
        AddListenerToButton(startButton, () => StartGame(), true);
        AddListenerToButton(restartButton, () => MasterManager.Instance.gameManager.RestartGame(), true);
        AddListenerToButton(continueButton, () => MasterManager.Instance.gameManager.TogglePause(), true);
        AddListenerToButton(quitButton, () => MasterManager.Instance.gameManager.EndGame(), true);

        ToggleCancelText(false);
        InitializeVolumeSlider();
        DeactivateButtons();
    }

    void AddListenerToButton(GameObject buttonGO, UnityAction listener, bool removePreviousListeners = false)
    {
        // Adds a listener to the specified button, and optionally removes all previous listeners

        Button button = buttonGO.GetComponent<Button>();

        if(removePreviousListeners) button.onClick.RemoveAllListeners();

        button.onClick.AddListener(listener);
    }

    void InitializeVolumeSlider()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.AddListener((v) => MasterManager.Instance.audioManager.AdjustMasterVolume(v));
        volumeSlider.value = 0.5f;
    }
    // ...Initialization functions

    // Activation/Deactivation of UI game objects...
    void ActivateButton(GameObject button)
    {
        button.SetActive(true);
    }

    void DeactivateButtons()
    {
        startButton.SetActive(false);
        restartButton.SetActive(false);
        continueButton.SetActive(false);
    }

    public void ToggleCancelText(bool active)
    {
        cancelTextGO.SetActive(active);
    }

    void ToggleInteractText(bool active)
    {
        interactTextGO.SetActive(active);
    }

    public void ToggleMessagesImage(bool active)
    {
        DeactivateButtons();
        messagesImage.gameObject.SetActive(active);
    }

    public void ToggleSettingsImage(bool active)
    {
        settingsImageGO.SetActive(active);

        if (active) ActivateButton(continueButton);
    }
    // ...Activation/Deactivation of UI game objects

    // Interact text control...
    public void ActivateInteractText(string text)
    {
        ToggleInteractText(true);
        interactTextTMP.SetText(text);
    }

    public void ActivateInteractText(string text, float writingDelay)
    {
        ToggleInteractText(true);
        DisplayText(interactTextTMP, text, writingDelay);
    }

    public void DeactivateInteractText()
    {
        interactTextTMP.SetText("");
        ToggleInteractText(false);
    }
    // ...Interact text control

    // Public coroutine triggers...
    public void PlayStartUI()
    {
        ToggleSettingsImage(false);
        StartCoroutine(PlayStartUIRoutine());
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public void PlayLevelEndingUI()
    {
        ToggleSettingsImage(false);
        ToggleMessagesImage(true);
        ActivateButton(restartButton);
        StartCoroutine(LevelEndingRoutine());
    }

    public void FadeImageAlpha(Image image, float targetAlpha, float fadingTime)
    {
        IsDoneFading = false;
        StartCoroutine(FadeImageAlphaRoutine(image, targetAlpha, fadingTime));
    }

    public void DisplayText(TextMeshProUGUI textBox, string text, float writingDelay)
    {
        IsDoneWriting = false;
        StartCoroutine(DisplayTextRoutine(textBox, text, writingDelay));
    }
    // ...Public coroutine triggers

    IEnumerator PlayStartUIRoutine()
    {
        // Runs the initial UI, displaying startMessage and activating the Start button

        ActivateInteractText(startMessage, startEndMessageDelay);
        yield return new WaitUntil(() => IsDoneWriting);

        ActivateButton(startButton);
    }

    IEnumerator StartGameRoutine()
    {
        // Hides the UI to start the game

        DeactivateButtons();

        ActivateInteractText("Press (ESC) to pause and access settings");
        yield return new WaitForSeconds(2f);

        DeactivateInteractText();

        FadeImageAlpha(messagesImage, 0f, startEndFadingTime);
        yield return new WaitUntil(() => IsDoneFading);

        ToggleMessagesImage(false);
        MasterManager.Instance.audioManager.StartPlayingWind();
        MasterManager.Instance.gameManager.CurrentGameStatus = GameManager.GameStatus.play;
    }

    IEnumerator LevelEndingRoutine()
    {
        // Activates the UI to display endMessage

        FadeImageAlpha(messagesImage, 0.85f, startEndFadingTime);
        yield return new WaitUntil(() => IsDoneFading);

        ActivateInteractText(endMessage, startEndMessageDelay);
    }

    IEnumerator DisplayTextRoutine(TextMeshProUGUI textBox, string text, float delay)
    {
        // Writes *text* in *textBox* one character at a time, waiting *delay* seconds before writing the next character

        string currentText;

        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i + 1);
            textBox.SetText(currentText);
            yield return new WaitForSeconds(delay);
        }

        IsDoneWriting = true;
    }

    IEnumerator FadeImageAlphaRoutine(Image image, float targetAlpha, float fadingTime)
    {
        // Changes an image's alpha value, taking *fadingTime* seconds to do it

        Color currentColor = image.color;
        float originalAlpha = currentColor.a;
        float step = 0f;

        while (step < 1f)
        {
            step += Time.deltaTime / fadingTime;
            currentColor.a = Mathf.Lerp(originalAlpha, targetAlpha, step);
            image.color = currentColor;
            yield return null;
        }

        IsDoneFading = true;
    }
}
