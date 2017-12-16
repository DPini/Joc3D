using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private FloorLoader floorLoader;

    private int[,] matrixLevel;

    public int lastPosUpdated;

	// Use this for initialization
	public void Start () {
        floorLoader = GameObject.Find("Level").GetComponent<FloorLoader>();
    }

    public void InitMap() {
        matrixLevel = floorLoader.InitializeFloor();
        for (int i = 0; i < 100; ++i) {
            for (int j = 0; j < 50; ++j)
            {
                Debug.Log("[" + i + "," + j + "] = " + matrixLevel[i, j]);
            }
        }
        Debug.Log(matrixLevel);
    }
}
