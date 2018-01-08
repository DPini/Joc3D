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

    private int level;

    private int freeRow;

    // Use this for initialization
    public Tile[,] InitializeFloor(out int lastPosUpdated)
    {
        level = 0;

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
           
            bool[][] decorateMatrix = DecorateZone(zoneSize, zoneType, level, prevZone);
            enemyController.createEnemiesZone(nRow, zoneSize, zoneType, level);
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
        if (lastZoneUpdated >= 100) level = 2;
        else if (lastZoneUpdated >= 50) level = 1;
        
        nRow = lastPos;
        int prevZone = matrixLevel[lastPos % 100,0].zone;
        while (nRow <= lastPos + 20)
        {
            int zoneType = GetRandomeZoneInt(prevZone);
            int zoneSize = createZone(zoneType);
            //Debug.Log("Created zone starting in " + nRow + "with size" + zoneSize);


            bool[][] decorateMatrix = DecorateZone(zoneSize, zoneType, level, prevZone);
            enemyController.createEnemiesZone(nRow, zoneSize, zoneType, level);
            nRow += zoneSize;
            //Debug.Log("Lastzone: " + lastZoneUpdated + " nRow: " + nRow);
            //Debug.Log("For de " + lastZoneUpdated % 100 + " a " + nRow % 100);
            //for (int i = lastZoneUpdated % 100; i < nRow % 100; ++i)
            for ( int i = 0; i < zoneSize; ++i)
            {
                int row = (lastZoneUpdated + i) % 100;
                for (int j = 0; j < nColumns; ++j)
                {  
                    matrixLevel[row, j] = zoneType;
                    //Debug.Log("i: " + i + " j: " + j + "i - lastZoneUpdated %100: " + (i - lastZoneUpdated % 100) );
                    //matrixLevel[i, j].isAccesible = !decorateMatrix[i - lastZoneUpdated % 100][j];    
                    matrixLevel[row, j].isAccesible = !decorateMatrix[i][j];
                }

            }

            lastZoneUpdated += zoneSize;

            prevZone = zoneType;
        }

        return lastZoneUpdated;
    }
    
    
    public void DestroyFloor() {
        Transform[] objectsToDestroy = GameObject.Find("Level").GetComponentsInChildren<Transform>();

        for (int i = objectsToDestroy.Length; i < objectsToDestroy.Length/2 ; ++i) {
            Destroy(objectsToDestroy[i].gameObject);
        }
    }

    public void DestroyFloor(float limit)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Ground");
        GameObject[] deco = GameObject.FindGameObjectsWithTag("Decoration");

        foreach (GameObject g in tiles)
        {
            if (g.transform.position.z <= limit) Destroy(g);
        }

        foreach (GameObject d in deco)
        {
            if (d.transform.position.z <= limit) Destroy(d);
        }


    }
    

    private bool[][] DecorateZone(int zoneSize, int zoneType, int level, int prevZone) {
        bool[][] decorateMatrix = new bool[zoneSize][];
        if ( prevZone != zoneType )
            freeRow = Random.Range(-6, 4);
        for (int i = 0; i < zoneSize; ++i)
        {
            decorateMatrix[i] = levelDecorator.DecorateRow(i + nRow, zoneType, level, freeRow);
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
                size = Random.Range(1, 3 + level);
                break;
            case 2: //river
                size = Random.Range(1, 2 + level);
                break;
            default: //safe-zone
                size = Random.Range(1, 2 + (2 - level));
                break;
        }
        return size;
    }

    private int GetRandomeZoneInt(int prevZone)
    {
        if (prevZone == -1) return 0;
        if (prevZone == 2 && level == 0) return 0;
        int rnd = Random.Range(0, 3);
        while (rnd == prevZone)
        {
            rnd = Random.Range(0, 3);
        }
        return rnd;
    }

    private void CreateRow(float position, GameObject floorInstance, int zoneType) {
        GameObject obj;
        for (int i = -12; i < 10; ++i) {
            Vector3 pos = new Vector3(i * tamFloor, 3.0f, position * tamFloor);
            if (Physics.Raycast(pos, Vector3.down, 10))
                Debug.Log("WE HAVE A VERY BIG PROBLEM. DOUBLE TILE BUG. POSITION: " + pos);
            obj = Instantiate(floorInstance, new Vector3(i * tamFloor, 0.0f, position * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            obj.transform.parent = gameObject.transform;
        }

        switch (zoneType) {
            case 1:
                break;
            case 2:
                platformController.CreatePlatformRow((int)position, level);
                break;
            case 3:
                break;
        }
    }

    private GameObject GetTile(int tile, int pos, int zoneSize) {
        GameObject ret = null;
        switch (tile) {
            case 1:
                if (level == 2) ret = roadFloorInstance[4];
                else if (zoneSize == 1) ret = roadFloorInstance[3];
                else if (pos == 0) ret = roadFloorInstance[0];
                else if (pos == zoneSize - 1) ret = roadFloorInstance[2];
                else ret = roadFloorInstance[1];
                break;
            case 2:
                ret = riverFloorInstance[level];
                break;
            default:
                if(pos % 2 == 0) ret = safeFloorInstance[level * 2 + 1];
                else ret = safeFloorInstance[level * 2 + 0];
                break;
        }
        return ret;
    }
}