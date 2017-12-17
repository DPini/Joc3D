using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public int maxEnemiesPerRow = 10;
    public int maxRows = 30;

    public GameObject model;

    private int lastCreatedEnemy;

    private Enemy[,] enemies;

	public void Init () {
        lastCreatedEnemy = 0;
        enemies = new Enemy[maxRows,maxEnemiesPerRow];

	}

    public void update(float deltaTime)
    {
        for ( int row = 0; row < maxRows; ++row)
        {
            for ( int enemy = 0; enemy < maxEnemiesPerRow; ++enemy)
            {
                enemies[row,enemy].update(deltaTime);
            }
        }
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
        int n = Random.Range(0, maxEnemiesPerRow);
        int row = pos % maxRows;

        for ( int i = 0; i < maxEnemiesPerRow; ++i)
        {
            if ( i <= n) {
                enemies[row, i].Init(model, pos, i*3);
            }
            else
            {
                enemies[row, i].disable();
            }
        }

        

    }


}
