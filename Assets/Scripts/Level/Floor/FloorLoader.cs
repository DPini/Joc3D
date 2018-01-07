using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoader : MonoBehaviour
{
    //Floor prefabs
    public GameObject[] safeFloorInstance;
    public GameObject[] roadFloorInstance;
    public GameObject[] riverFloorInstance;


    private int nRow;
    private int nColumns = 50;

    private float tamFloor = 1.5f;

    private LevelDecorator levelDecorator;
    private EnemyController enemyController;
    private PlatformController platformController;

    // Use this for initialization
    public Tile[,] InitializeFloor(out int lastPosUpdated)
    {
        Tile[,] matrixLevel = new Tile[100,50];
        int lastZoneUpdated = -4;
        levelDecorator = GameObject.Find("Level").GetComponent<LevelDecorator>();
        enemyController = GameObject.Find("Controllers").GetComponent<EnemyController>();
        platformController = GameObject.Find("Controllers").GetComponent<PlatformController>();
        platformController.Init();


        nRow = -4;
        int prevZone = -1;
        while(nRow <= 50)
        {
            int zoneType = GetRandomeZoneInt(prevZone);
            int zoneSize = createZone(zoneType);
           
            bool[][] decorateMatrix = DecorateZone(zoneSize, zoneType);
            enemyController.createEnemiesZone(nRow, zoneSize, zoneType);
            nRow += zoneSize;

            for (int i = lastZoneUpdated; i < nRow; ++i) {
                for (int j = 0; j < nColumns; ++j) {
                    if (i < 0)
                    {
                        matrixLevel[100 + i, j] = zoneType;
                        matrixLevel[100 + i, j].isAccesible = !decorateMatrix[i - lastZoneUpdated][j];
                    }
                    else
                    {
                        matrixLevel[i, j] = zoneType;
                        matrixLevel[i, j].isAccesible = !decorateMatrix[i - lastZoneUpdated][j];
                    }
                }
               
            }
                  
            lastZoneUpdated += zoneSize;

            prevZone = zoneType;
        }
        lastPosUpdated = lastZoneUpdated;
        return matrixLevel;
    }

    public int UpdateFloor(Tile[,] matrixLevel, int lastPos) {    
        int lastZoneUpdated = lastPos;
        
        nRow = lastPos;
        int prevZone = matrixLevel[lastPos % 100,0].zone;
        while (nRow <= lastPos + 25)
        {
            int zoneType = GetRandomeZoneInt(prevZone);
            int zoneSize = createZone(zoneType);

            bool[][] decorateMatrix = DecorateZone(zoneSize, zoneType);
            enemyController.createEnemiesZone(nRow, zoneSize, zoneType);
            nRow += zoneSize;

            for (int i = lastZoneUpdated % 100; i < nRow % 100; ++i)
            {
                for (int j = 0; j < nColumns; ++j)
                {  
                    matrixLevel[i, j] = zoneType;
                    matrixLevel[i, j].isAccesible = !decorateMatrix[i - lastZoneUpdated % 100][j];    
                }

            }

            lastZoneUpdated += zoneSize;

            prevZone = zoneType;
        }

        return lastZoneUpdated;
    }
    
    /**
    public void DestroyFloor() {
        Transform[] objectsToDestroy = GameObject.Find("Level").GetComponentsInChildren<Transform>();

        for (int i = 0; i < objectsToDestroy.Length; ++i) {
            Destroy(objectsToDestroy[i].gameObject);
        }
    }
    */

    private bool[][] DecorateZone(int zoneSize, int zoneType) {
        bool[][] decorateMatrix = new bool[zoneSize][];
        for (int i = 0; i < zoneSize; ++i)
        {
            decorateMatrix[i] = levelDecorator.DecorateRow(i + nRow, zoneType);
        }
        return decorateMatrix;
    }

    private int createZone(int zoneType) {
        int zoneSize = GetRandomZoneSize(zoneType);
        if (nRow < 0) zoneSize += 4;
        for (int i = 0; i < zoneSize; ++i)
        {
            GameObject floorInstance = GetTile(zoneType, i, zoneSize);
            CreateRow(i + nRow, floorInstance, zoneType);
        }
        return zoneSize;

    }

    private int GetRandomZoneSize(int type)
    {
        int size;
        switch (type) { 
            case 1: //road
                size = Random.Range(1, 6);
                break;
            case 2: //river
                size = Random.Range(1, 4);
                break;
            default: //safe-zone
                size = Random.Range(1, 3);
                break;
        }
        return size;
    }

    private int GetRandomeZoneInt(int prevZone)
    {
        if (prevZone == -1) return 0;
        int rnd = Random.Range(0, 3);
        while (rnd == prevZone)
        {
            rnd = Random.Range(0, 3);
        }
        return rnd;
    }

    private void CreateRow(float position, GameObject floorInstance, int zoneType) {
        GameObject obj;
        for (int i = -30; i < 20; ++i) {
            obj = Instantiate(floorInstance, new Vector3(i * tamFloor, 0.0f, position * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            obj.transform.parent = gameObject.transform;
        }

        switch (zoneType) {
            case 1:
                break;
            case 2:
                platformController.CreatePlatformRow((int)position);
                break;
            case 3:
                break;
        }
    }

    private GameObject GetTile(int tile, int pos, int zoneSize) {
        GameObject ret = null;
        switch (tile) {
            case 1:
                //if(pos == 0)
                //if(pos == zoneSize - 1)
                ret = roadFloorInstance[0];
                break;
            case 2:
                ret = riverFloorInstance[0];
                break;
            default:
                if(pos % 2 == 0) ret = safeFloorInstance[1];
                else ret = safeFloorInstance[0];
                break;
        }
        return ret;
    }
}