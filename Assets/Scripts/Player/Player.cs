using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameObject playerInstance;
    private PlayerMovement playerMovement;


    public void Init()
    {
        playerInstance = GameObject.Find("Player");
        playerMovement = playerInstance.GetComponent<PlayerMovement>();
    }

    public Vector2Int getTile()
    {
        return Utils.coordsToTile(playerInstance.transform.position);
    }

    public Vector3 getPosition()
    {
        return playerInstance.transform.position;
    }

    public void Jump (Directions toMove) {
        playerMovement.Jump(toMove);
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
