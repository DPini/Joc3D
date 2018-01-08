using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    

    public int maxEnemiesPerRow = 4;
    public int maxRows = 75;

    public GameObject[] models;

    private int lastCreatedEnemy;

    private float tamFloor = 1.5f;

    private GameObject[,] enemies;

	public void Init () {
        lastCreatedEnemy = 0;
        enemies = new GameObject[maxRows,maxEnemiesPerRow];
        

	}


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
                for (int i = nRow; i < nRow + zoneSize; ++i)
                {
                    deleteEnemies(i);
                }
                break;
        }
    }

    void createEnemies( int pos, int level )
    {
        int row = ((pos % maxRows) + maxRows) % maxRows;
        int n = Random.Range(2, maxEnemiesPerRow);

        GameObject model = models[Random.Range(0, 2) + level * 2];

        float distanceEnemies = Random.Range(3.0f, 5.0f);

        float initSequence = Random.Range(-8.0f, 0.0f);
        for ( int i = 0; i < maxEnemiesPerRow; ++i)
        {
            Destroy(enemies[row, i]);
            if ( i <= n ) {
                float height = 0.3f;
                
                enemies[row, i] = Instantiate(model, new Vector3(initSequence + (distanceEnemies * (tamFloor * 1.5f) * i), height, pos * tamFloor), new Quaternion(0.0f, Mathf.PI , 0.0f, 0.0f)) as GameObject;
                if (model.name != "Bus")
                {
                    enemies[row, i].transform.Rotate(0.0f, -90.0f, 0.0f);
                    if(model.name.Contains("alien")) enemies[row, i].transform.Rotate(0.0f, 180.0f, 0.0f);
                }
            }
        }
    }

    void deleteEnemies(int row)
    {
        row = ((row % maxRows) + maxRows) % maxRows;
        for (int i = 0; i < maxEnemiesPerRow; ++i) Destroy(enemies[row, i]);

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
            Rigidbody rb = Utils.GetComponentAddIfNotExists<Rigidbody>(gameObj);
            rb.isKinematic = false;
            gameObj.GetComponent<BoxCollider>().isTrigger = false;
            gameObj.GetComponent<Enemy>().setVelocity(0);
        }

    }


}
