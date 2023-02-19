using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : Singleton<GameManager>
{
    GameObject mainCam;
    GameObject secondaryCam;
    public bool AllowPlayerInput { get; private set; }

    public enum GameStatus {start, play, pause, interact, levelEnding, end};
    private GameStatus gameStatus;
    public GameStatus CurrentGameStatus
    {
        get { return gameStatus; }

        set
        {
            if (gameStatus == value) return;

            gameStatus = value;
            ChangeCursorLockState(gameStatus == GameStatus.play ? CursorLockMode.Locked : CursorLockMode.None);
            AllowPlayerInput = gameStatus == GameStatus.play;

            switch (gameStatus)
            {
                case GameStatus.pause: //Pause the game
                    PauseGame();
                    break;

                case GameStatus.play: //Unpause the game
                    UnpauseGame();
                    break;

                case GameStatus.levelEnding:
                    MasterManager.Instance.uiManager.PlayLevelEndingUI();
                    break;

                case GameStatus.end: //Quit application
                    ExitGame();
                    break;
            }
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Update()
    {
        //Listen for the (Esc) key to pause the game
        if (MasterManager.Instance.inputManager.IsCancelPerformed) TogglePause();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.isLoaded)
        {
            GetReferences();
            MasterManager.Instance.uiManager.GetReferences();
            MasterManager.Instance.audioManager.GetReferences();
            CurrentGameStatus = GameStatus.start;
            MasterManager.Instance.uiManager.PlayStartUI();
        }
    }

    void GetReferences()
    {
        mainCam = GameObject.Find("Main Camera");
        secondaryCam = GameObject.Find("Secondary Camera");
        if (mainCam == null) Debug.Log($"WARNING! mainCam is null");
        if (secondaryCam == null) Debug.Log($"WARNING! secondaryCam is null");
        ToggleCamera(secondaryCam, false);
    }

    public void TogglePause()
    {
        switch (gameStatus)
        {
            case GameStatus.play: //Pause the game
                CurrentGameStatus = GameStatus.pause;
                break;

            case GameStatus.pause: //Unpause the game
                CurrentGameStatus = GameStatus.play;
                break;
        }
    }

    void PauseGame()
    {
        MasterManager.Instance.uiManager.ToggleMessagesImage(true);
        MasterManager.Instance.uiManager.ToggleSettingsImage(true);
        Time.timeScale = 0;
    }

    void UnpauseGame()
    {
        Time.timeScale = 1;
        MasterManager.Instance.uiManager.ToggleSettingsImage(false);
        MasterManager.Instance.uiManager.ToggleMessagesImage(false);
    }

    void ChangeCursorLockState(CursorLockMode state)
    {
        Cursor.lockState = state;
        Cursor.visible = state == CursorLockMode.None;
    }

    public void SwitchToMainCam()
    {
        if (mainCam.activeSelf) return;

        ToggleCamera(secondaryCam, false);
        ToggleCamera(mainCam, true);
        CurrentGameStatus = GameStatus.play;
    }

    public void SwitchToSecondaryCam()
    {
        if (secondaryCam.activeSelf) return;

        ToggleCamera(mainCam, false);
        ToggleCamera(secondaryCam, true);
        secondaryCam.transform.localPosition = mainCam.transform.localPosition;
        secondaryCam.transform.localRotation = mainCam.transform.localRotation;
    }

    void ToggleCamera(GameObject camera, bool active)
    {
        camera.SetActive(active);
    }

    public void RestartGame()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        CurrentGameStatus = GameStatus.end;
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
