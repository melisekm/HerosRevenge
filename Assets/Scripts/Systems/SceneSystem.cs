using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    public PlayerContainer playerContainer;

    protected override void Awake()
    {
        if (Instance == null) {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
 
        } else if (Instance != this) {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Debug.Log(Instance.playerContainer.currentLevel);
            playerContainer = Instance.playerContainer;
            playerContainer.currentLevel++;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        if(playerContainer == null)
        {
            playerContainer = new PlayerContainer();
        }
        Debug.Log(Instance.playerContainer.currentLevel);
        Debug.Log("SceneSystem Start");

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
