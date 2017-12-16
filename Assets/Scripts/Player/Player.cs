using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    PlayerMovement playerMovement;

    public void Init() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void Jump (Directions toMove) {
        playerMovement.Jump(toMove);
	}

    public void update()
    {
        playerMovement.Physics_update();
    }
}
