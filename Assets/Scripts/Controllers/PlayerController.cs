using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Player player;
    

	public void Init () {
        player = GameObject.Find("PlayerClass").GetComponent<Player>();
        player.Init();
	}

    public void update()
    {
        player.update();
    }

    public void Jump(Directions toMove) {
        player.Jump(toMove);
    }

    public Position GetPosition() {
        return player.GetPosition();
        
    }
}
