using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelController {

    private FloorLoader floorLoader;

    public int[,] matrixLevel;
    public int lastPosUpdated;

	// Use this for initialization
	public void Start () {
        floorLoader = new FloorLoader();
    }

    public void LoadPrefabs(GameObject safeFloor, GameObject roadFloor) {
        floorLoader.LoadPrefabs(safeFloor, roadFloor);
    }

    public void InitFloor() {
        matrixLevel = floorLoader.InitializeFloor();

        Debug.Log(matrixLevel);
    }
}
