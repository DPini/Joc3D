﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenuController : MonoBehaviour {

    private GameObject deadPanel;
    private GameController gameController;
    private AudioController audioController;
    private bool isMenuShown;
    

	// Use this for initialization
	public void Init() {
        gameController = GameObject.Find("Controllers").GetComponent<GameController>();
        deadPanel = GameObject.Find("DeadPanel");
        isMenuShown = false;
        deadPanel.SetActive(isMenuShown);
        audioController = GameObject.Find("Music").GetComponent<AudioController>();

    }

    public void showMenu()
    {
        if (!isMenuShown)
        { 
            deadPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("RestartGameBtn"));
            isMenuShown = true;
            audioController.EnterMenu();
        }
    }

    public void RestartGameHandler()
    {
        gameController.restartGame();
    }

    public void ExitToGameMenuHandler()
    {
        Debug.Log("Main menu not implemented yet!");
    }

    public void ExitToDesktopHandler()
    {
        gameController.exitGame();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
