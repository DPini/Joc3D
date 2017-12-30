using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject controllers;

    private LevelController levelController;
    private PlayerController playerController;

    // Use this for initialization
    void Start () {
        controllers = gameObject;
        levelController = controllers.GetComponent<LevelController>();
        playerController = controllers.GetComponent<PlayerController>();

        levelController.Init();
        playerController.Init();

        levelController.InitMap();
	}
	
	// Update is called once per frame
	void Update () {
        int lastPosUpdated = levelController.GetLastPosUpdated();
        if (playerController.GetPosition().z > lastPosUpdated - 25)
        {
            Debug.Log("Updating with player in: " + playerController.GetPosition().z + " with lastPos: " + lastPosUpdated);
            levelController.UpdateMap();
        }
        
            
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerController.Jump(Directions.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerController.Jump(Directions.right);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            playerController.Jump(Directions.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerController.Jump(Directions.down);
        }
        else playerController.update();
    }
}
