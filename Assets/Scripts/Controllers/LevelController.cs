using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private FloorLoader floorLoader;

    private Tile[,] matrixLevel;

    private int lastPosUpdated;
    private int loops;

    // Use this for initialization
    public void Init () {
        loops = 0;
        floorLoader = GameObject.Find("Level").GetComponent<FloorLoader>();
    }

    public void InitMap() {
        matrixLevel = floorLoader.InitializeFloor();

        // for (int i = 0; i < 100; ++i) {
        //     for (int j = 0; j < 50; ++j)
        //     {
        //         Debug.Log("[" + i + "," + j + "] = " + matrixLevel[i, j].zone + ", " + matrixLevel[i, j].isAccesible);
        //     }
        // }
        
        lastPosUpdated = 50;
    }

    public void UpdateMap() {
        floorLoader.UpdateFloor(matrixLevel, lastPosUpdated);
        lastPosUpdated += 25;
    }

    public int GetLastPosUpdated() {
        return lastPosUpdated;
    }
}
