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
        if (matrixLevel[row, col].isAccesible) return col > 23 && col < 34;
        else return false;
        
    }

    public int GetNextZoneTile(Vector2Int tile) {
        return GetNextZoneTile(tile.x, tile.y);
    }

    public int GetNextZoneTile(int row, int col)
    {
        return matrixLevel[row, col].zone;
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

    public void AddPhysics()
    {
        //foreach (Transform t in GameObject.Find("Level").transform)
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Ground"))
        {
            Rigidbody rb = Utils.GetComponentAddIfNotExists<Rigidbody>(gameObj);
            rb.isKinematic = false;
            rb.useGravity = false;
        }

        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Decoration"))
        {
            Rigidbody rb = Utils.GetComponentAddIfNotExists<Rigidbody>(gameObj);
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.mass = 0.02f;
            gameObject.AddComponent<BoxCollider>();
            BoxCollider boxc = Utils.GetComponentAddIfNotExists<BoxCollider>(gameObj);
            boxc.enabled = true;

        }

    }
}
