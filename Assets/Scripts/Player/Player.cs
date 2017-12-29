using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    PlayerMovement playerMovement;
    GameObject player;

    public void Init() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player = GameObject.Find("Player");
    }

    public Vector2Int getTile()
    {
        return Utils.coordsToTile(player.transform.position);
    }

    public Vector3 getPosition()
    {
        return player.transform.position;
    }

    public void Jump (Directions toMove) {
        playerMovement.Jump(toMove);
	}

    public void update()
    {
        playerMovement.Physics_update();
    }
}
