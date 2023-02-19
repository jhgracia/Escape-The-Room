using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : PersistentSingleton<MasterManager>
{
    //Managers instances
    public GameManager gameManager { get; private set; }
    public InputManager inputManager { get; private set; }
    public AudioManager audioManager { get; private set; }
    public UIManager uiManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        GetChildrenManagers();
    }

    void GetChildrenManagers()
    {
        gameManager = GetComponentInChildren<GameManager>();
        inputManager = GetComponentInChildren<InputManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        uiManager = GetComponentInChildren<UIManager>();
    }
}
