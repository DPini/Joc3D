using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoader : MonoBehaviour
{

    public GameObject safeFloorInstance;
    public GameObject roadFloorInstance;

    private int tamFloor = 1;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 100; ++i)
        {
            CreateRow(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //do nothing
    }

    private void CreateRow(float position) {
        GameObject floorInstance = GetRandomTile();
        GameObject obj;
        for (int i = -10; i < 10; ++i) {
            obj = Instantiate(floorInstance, new Vector3(i * tamFloor, 0.0f, position * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            obj.transform.parent = gameObject.transform;
        } 
    }

    private GameObject GetRandomTile() {
        GameObject ret;
        int rnd = Random.Range(0, 2);
        Debug.Log("rand: " + rnd);
        switch (rnd) {
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