using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject controllers;

    private LevelController levelController;
    private PlayerController playerController;
    private EnemyController enemyController;

    // Use this for initialization
    void Start () {
        controllers = gameObject;
        levelController = controllers.GetComponent<LevelController>();
        playerController = controllers.GetComponent<PlayerController>();
        enemyController = controllers.GetComponent<EnemyController>();

        enemyController.Init();
        levelController.Init();
        playerController.Init();



        levelController.InitMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (levelController.IsTileAccessible(playerController.getNextTile(Directions.left)))
            {
                playerController.Jump(Directions.left);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (levelController.IsTileAccessible(playerController.getNextTile(Directions.right)))
            {
                playerController.Jump(Directions.right);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (levelController.IsTileAccessible(playerController.getNextTile(Directions.up)))
            {
                playerController.Jump(Directions.up);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (levelController.IsTileAccessible(playerController.getNextTile(Directions.down)))
            {
                playerController.Jump(Directions.down);
            }
        }
        else playerController.update();

        //enemyController.update(Time.deltaTime);

    }
}
