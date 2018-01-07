using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public int maxEnemiesPerRow = 40;
    public int maxRows = 30;

    public GameObject[] models;

    private int lastCreatedEnemy;

    private float tamFloor = 1.5f;

    private GameObject[,] enemies;

	public void Init () {
        lastCreatedEnemy = 0;
        enemies = new GameObject[maxRows,maxEnemiesPerRow];
        

	}

    // public void update(float deltaTime)
    // {
    //     for ( int row = 0; row < maxRows; ++row)
    //     {
    //         for ( int enemy = 0; enemy < maxEnemiesPerRow; ++enemy)
    //         {
    //             if (enemies[row,enemy])
    //             enemies[row,enemy].update(deltaTime);
    //         }
    //     }
    // }

    public void createEnemiesZone( int nRow, int zoneSize, int zoneType, int level )
    {
        switch (zoneType)
        {
            case 1:
                for ( int i = nRow; i < nRow + zoneSize ; ++i)
                {
                    createEnemies(i, level);
                }
                break;
            default:
                break;
        }
    }

    void createEnemies( int pos, int level )
    {
        int n = Random.Range(0, maxEnemiesPerRow);
        int row = ((pos % maxRows) + maxRows) % maxRows;

        GameObject model = models[Random.Range(models.Length / 2 * level, models.Length / 2 * level + 2)];

        float lastPos = 1.5f;
        for ( int i = 0; i < maxEnemiesPerRow; ++i)
        {
            if ( i <= n) {
                lastPos += Random.Range(4.5f, 9.0f);
                
                float height = 0.0f;
                if (model.name == "Bus") height = 0.15f;
                
                enemies[row, i] = Instantiate(model, new Vector3(lastPos, height, pos * tamFloor), new Quaternion(0.0f, Mathf.PI , 0.0f, 0.0f)) as GameObject;
                if (model.name != "Bus") enemies[row, i].transform.Rotate(0.0f, 90.0f, 0.0f);
            }
            else
            {
                Destroy(enemies[row, i]);
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
