﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private GameObject controllers;

    private LevelController levelController;
    private PlayerController playerController;
    private EnemyController enemyController;
    private ScoreController scoreController;
    private GameMenuController gameMenuController;
    private InputController inputController;

    enum GameStates { Playing, Died };
    private GameStates gameState;

    // Use this for initialization
    void Start () {
        controllers = gameObject;
        levelController = controllers.GetComponent<LevelController>();
        playerController = controllers.GetComponent<PlayerController>();
        enemyController = controllers.GetComponent<EnemyController>();
        scoreController = controllers.GetComponent<ScoreController>();
        gameMenuController = controllers.GetComponent<GameMenuController>();
        inputController = controllers.GetComponent<InputController>();


        levelController.Init();
        enemyController.Init();
        playerController.Init();
        scoreController.Init();
        gameMenuController.Init();




        levelController.InitMap();

        gameState = GameStates.Playing;
	}

    public void restartGame()
    {
        SceneManager.LoadScene("FirstScene");
    }
	
	// Update is called once per frame
	void Update () {
        int lastPosUpdated = levelController.GetLastPosUpdated();
        if (playerController.GetPosition().z > lastPosUpdated - 25)
        {
            Debug.Log("Updating with player in: " + playerController.GetPosition().z + " with lastPos: " + lastPosUpdated);
            levelController.ToUpdateMap();
        }

        scoreController.updateScore(playerController.GetPosition());
            
	}

    void FixedUpdate()
    {
        if (gameState == GameStates.Playing)
        {

            if (inputController.checkInput(KeyCode.LeftArrow))
            {
                if (levelController.IsTileAccessible(playerController.getNextTile(Directions.left)))
                {
                    playerController.Jump(Directions.left);
                }
            }
            else if (inputController.checkInput(KeyCode.RightArrow))
            {
                if (levelController.IsTileAccessible(playerController.getNextTile(Directions.right)))
                {
                    playerController.Jump(Directions.right);
                }
            }
            else if (inputController.checkInput(KeyCode.UpArrow))
            {
                if (levelController.IsTileAccessible(playerController.getNextTile(Directions.up)))
                {
                    playerController.Jump(Directions.up);
                }
            }
            else if (inputController.checkInput(KeyCode.DownArrow))
            {
                if (levelController.IsTileAccessible(playerController.getNextTile(Directions.down)))
                {
                    playerController.Jump(Directions.down);
                }
            }
            else playerController.update();
        }

        //enemyController.update(Time.deltaTime);

    }

    public void endGame()
    {
        gameMenuController.showMenu();
        gameState = GameStates.Died;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
