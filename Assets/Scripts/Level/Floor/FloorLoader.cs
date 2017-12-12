using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoader : MonoBehaviour
{

    public GameObject safeFloorInstance;
    public GameObject roadFloorInstance;

    private int nRow;

    private float tamFloor = 1.5f;
    // Use this for initialization
    void Start()
    {
        nRow = -4;
        int prevZone = -1;
        while(nRow < 40)
        {
            int zoneType = GetRandomeZoneInt(prevZone);
            nRow += createZone(zoneType);

            prevZone = zoneType;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //do nothing
    }

    private int createZone(int zoneType) {
        int zoneSize = GetRandomZoneSize(zoneType);
        if (nRow < 0) zoneSize += 4;
        GameObject floorInstance = GetTile(zoneType);
        for (int i = nRow; i < nRow + zoneSize; ++i) {
            CreateRow(i, floorInstance);
            gameObject.GetComponent<LevelDecorator>().DecorateRow(i, zoneType);
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
        for (int i = -20; i < 20; ++i) {
            obj = Instantiate(floorInstance, new Vector3(i * tamFloor, 0.0f, position * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            obj.transform.parent = gameObject.transform;
        } 
    }

    private GameObject GetTile(int tile) {
        GameObject ret;
        switch (tile) {
            case 1:
                ret = roadFloorInstance;
                break;
            default:
                ret = safeFloorInstance;
                break;
        }
        return ret;
    }
}