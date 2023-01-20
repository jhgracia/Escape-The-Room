using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    //Master manager instance
    public static MasterManager main { get; private set; }

    //Managers instances
    public GameManager gameManager { get; private set; }
    public InputManager inputManager { get; private set; }
    public AudioManager audioManager { get; private set; }

    private void Awake()
    {
        if(main != null && main != this)
        {
            Destroy(this);
            return;
        }

        main = this;
        DontDestroyOnLoad(this);
        GetChildrenManagers();
    }

    void GetChildrenManagers()
    {
        gameManager = GetComponentInChildren<GameManager>();
        inputManager = GetComponentInChildren<InputManager>();
        audioManager = GetComponentInChildren<AudioManager>();
    }
}
