using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorLoader : MonoBehaviour
{
    //Floor prefabs
    public GameObject safeFloorInstance;
    public GameObject roadFloorInstance;

    private int nRow;
    private int nColumns = 40;

    private float tamFloor = 1.5f;

    private LevelDecorator levelDecorator;

    // Use this for initialization
    public int[,] InitializeFloor()
    {
        int[,] matrixLevel = new int[100,50];
        int lastZoneUpdated = -4;
        levelDecorator = GameObject.Find("Level").GetComponent<LevelDecorator>();

        nRow = -4;
        int prevZone = -1;
        while(nRow <= 50)
        {
            int zoneType = GetRandomeZoneInt(prevZone);
            int zoneSize = createZone(zoneType);
            nRow += zoneSize;

            for (int i = lastZoneUpdated; i <= nRow; ++i) {
                for (int j = 0; j < nColumns; ++j) {
                    if (i < 0) matrixLevel[100 + i,j] = zoneType;
                    else matrixLevel[i,j] = zoneType;
                }
               
            }

            lastZoneUpdated += zoneSize;

            prevZone = zoneType;
        }

        return matrixLevel;
    }

    private int createZone(int zoneType) {
        int zoneSize = GetRandomZoneSize(zoneType);
        if (nRow < 0) zoneSize += 4;

        for (int i = 0; i < zoneSize; ++i) {
            GameObject floorInstance = GetTile(zoneType, i, zoneSize);
            CreateRow(i + nRow, floorInstance);
            levelDecorator.DecorateRow(i + nRow, zoneType);
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
            default: //safe-zone
                size = Random.Range(1, 3);
                break;
        }
        return size;
    }

    private int GetRandomeZoneInt(int prevZone)
    {
        if (prevZone == -1) return 0;
        int rnd = Random.Range(0, 2);
        while (rnd == prevZone)
        {
            rnd = Random.Range(0, 2);
        }
        return rnd;
    }

    private void CreateRow(float position, GameObject floorInstance) {
        GameObject obj;
        for (int i = -35; i < 20; ++i) {
            obj = Instantiate(floorInstance, new Vector3(i * tamFloor, 0.0f, position * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            //obj.transform.parent = gameObject.transform;
        } 
    }

    private GameObject GetTile(int tile, int pos, int zoneSize) {
        GameObject ret = null;
        switch (tile) {
            case 1:
                //if(pos == 0)
                //if(pos == zoneSize - 1)
                ret = roadFloorInstance;
                break;
            default:
                //if(pos % 2 == 0)
                ret = safeFloorInstance;
                break;
        }
        return ret;
    }
}