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

    public Vector2Int getNextTile( Directions d )
    {
        Vector3 pos = player.getPosition() + PlayerMovement.direction_vector(d)*1.5f;

        Vector2Int v = Utils.coordsToTile(pos);
        //Debug.Log("Next jump: " + v);
        return v;
    }

    public void Jump(Directions toMove, int nextZone) {
        player.Jump(toMove, nextZone);
    }

    public Position GetPosition() {
        return player.GetPosition();
        
    }
}
