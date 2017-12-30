using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private FloorLoader floorLoader;

    private Tile[,] matrixLevel;

    private int lastPosUpdated;
    private int loops;

    private bool toUpdate = false;

    // Use this for initialization
    public void Init () {
        loops = 0;
        floorLoader = GameObject.Find("Level").GetComponent<FloorLoader>();
    }

    public bool IsTileAccessible(Vector2Int tile)
    {
        return IsTileAccessible(tile.x, tile.y);
    }

    public bool IsTileAccessible(int row, int col)
    {
        return matrixLevel[row,col].isAccesible;
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

    public void ToUpdateMap() {
        toUpdate = true;
    }

    public void UpdateMap() {
        floorLoader.UpdateFloor(matrixLevel, lastPosUpdated);
        lastPosUpdated += 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (toUpdate) {
            UpdateMap();
            toUpdate = false;
        }


    }

    public int GetLastPosUpdated() {
        return lastPosUpdated;
    }
}
