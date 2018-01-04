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
        int n = Random.Range(0, maxEnemiesPerRow);
        int row = pos % maxRows;

        float lastPos = 1.5f;
        for ( int i = 0; i < maxEnemiesPerRow; ++i)
        {
            if ( i <= n) {
                lastPos += Random.Range(1.5f, 6.0f);
                GameObject model = models[Random.Range(0, models.Length)];
                float height = 0.0f;
                if (model.name == "Bus") height = 0.15f;
                enemies[row, i] = Instantiate(model, new Vector3(lastPos, height, pos * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            }
            else
            {
                Destroy(enemies[row, i]);
            }
        }

        

    }


}
