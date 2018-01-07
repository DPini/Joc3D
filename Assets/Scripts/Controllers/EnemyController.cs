using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public int maxEnemiesPerRow = 15;
    public int maxRows = 60;

    public GameObject[] models;

    private int lastCreatedEnemy;

    private float tamFloor = 1.5f;

    private GameObject[,] enemies;

	public void Init () {
        lastCreatedEnemy = 0;
        enemies = new GameObject[maxRows,maxEnemiesPerRow];
        

	}

    public void createEnemiesZone( int nRow, int zoneSize, int zoneType )
    {
        switch (zoneType)
        {
            case 1:
                for ( int i = nRow; i < nRow + zoneSize ; ++i)
                {
                    createEnemies(i);
                }
                break;
            default:
                break;
        }
    }

    void createEnemies( int pos )
    {
        int row = ((pos % maxRows) + maxRows) % maxRows;
        int n = Random.Range(2, maxEnemiesPerRow);

        float lastPos = 1.5f;
        for ( int i = 0; i < maxEnemiesPerRow; ++i)
        {
            Destroy(enemies[row, i]);
            if ( i <= n ) {
                lastPos += Random.Range(1.8f, 6.0f);
                GameObject model = models[Random.Range(0, models.Length)];
                float height = 0.0f;
                if (model.name == "Bus") height = 0.15f;
                enemies[row, i] = Instantiate(model, new Vector3(lastPos, height, pos * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            }
        }

        

    }

    public void AddPhysics()
    {
        /*
        for ( int row = 0; row < maxRows; ++row)
        {
            for ( int enemy = 0; enemy < maxEnemiesPerRow; ++enemy)
            {
                if (enemies[row, enemy])
                {
                    GameObject e = enemies[row, enemy];
                    e.AddComponent<Rigidbody>();
                    e.GetComponent<Enemy>().setVelocity(0);
                }
            }
        }
        */

        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //gameObj.AddComponent<Rigidbody>();
            Utils.GetComponentAddIfNotExists<Rigidbody>(gameObj);
            gameObj.GetComponent<BoxCollider>().isTrigger = false;
            gameObj.GetComponent<Enemy>().setVelocity(0);
        }

    }


}
