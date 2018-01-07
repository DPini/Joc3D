using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameObject playerInstance;
    private PlayerMovement playerMovement;
    private PlayerEnemyCollision playerEnemyCollision;
    Color origColor;
    private bool godMode;


    public void Init()
    {
        playerInstance = GameObject.Find("Player");
        playerMovement = playerInstance.GetComponent<PlayerMovement>();
        playerEnemyCollision = playerInstance.GetComponent<PlayerEnemyCollision>();
        origColor = playerInstance.GetComponentInChildren<MeshRenderer>().material.color;
        godMode = false;
    }

    public void killPlayer()
    {
        playerMovement.killPlayer();
    }

    public void setGodMode(bool b)
    {
        godMode = b;
        playerMovement.setGodMode(godMode);
        playerEnemyCollision.setGodMode(godMode);
        if (godMode)
        {
            playerInstance.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            playerInstance.GetComponentInChildren<MeshRenderer>().material.color = origColor;
        }
    }

    public bool getGodMode()
    {
        return godMode;
    }

    public Vector2Int getTile()
    {
        return Utils.coordsToTile(playerInstance.transform.position);
    }

    public Vector3 getPosition()
    {
        return playerInstance.transform.position;
    }

    public void Jump (Directions toMove, int nextZone) {
        playerMovement.Jump(toMove, nextZone);
	}

    public void update()
    {
        playerMovement.Physics_update();
    }

    public Position GetPosition() {
        Position pos;
        pos.x = playerInstance.transform.position.x / 1.5f + 20;
        pos.z = playerInstance.transform.position.z / 1.5f;
        return pos;
    }
}
