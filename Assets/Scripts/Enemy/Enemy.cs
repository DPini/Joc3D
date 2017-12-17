using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject enemy;

    private float velocity;

    private float tamFloor = 1.5f;

    public void Init(GameObject model, int row, int pos)
    {
        enemy = Instantiate(model, new Vector3(pos * tamFloor, 0.0f, row * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
        if (row % 2 == 0)
        {
            velocity *= -1;
            enemy.transform.Rotate(0.0f, 0.0f, 180.0f);
        }
    }

    public void disable()
    {
        Destroy(enemy);
    }
	
	public void update (float deltaTime) {
        if (enemy)
        {
            enemy.transform.transform.position += new Vector3(deltaTime * velocity, 0, 0);
        }
	}
}
