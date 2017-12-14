using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelController {

    struct Tile {
        int tile;
        bool isDecorated;
    }

    private FloorLoader floorLoader;

    private int[,] matrixLevel;

    public int lastPosUpdated;

	// Use this for initialization
	public void Start () {
        floorLoader = new FloorLoader();
    }

    public void LoadPrefabs(GameObject safeFloor, GameObject roadFloor) {
        floorLoader.LoadPrefabs(safeFloor, roadFloor);
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
