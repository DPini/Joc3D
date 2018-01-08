using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private FloorLoader floorLoader;

    private Tile[,] matrixLevel;

    private int lastPosUpdated = 50;
    private int loops;

    private bool toUpdate = false;

    private bool toDestroy = false;

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
        row = ((row % 100) + 100 ) % 100;
        if (matrixLevel[row, col].isAccesible) return col > 23 && col < 34;
        else return false;
        
    }

    public int GetNextZoneTile(Vector2Int tile) {
        return GetNextZoneTile(tile.x, tile.y);
    }

    public int GetNextZoneTile(int row, int col)
    {
        row = ((row % 100) + 100) % 100;
        return matrixLevel[row, col].zone;
    }

    public void InitMap() {
        int lastPosUpd;
        matrixLevel = floorLoader.InitializeFloor(out lastPosUpd);

        // for (int i = 0; i < 100; ++i) {
        //     for (int j = 0; j < 50; ++j)
        //     {
        //         Debug.Log("[" + i + "," + j + "] = " + matrixLevel[i, j].zone + ", " + matrixLevel[i, j].isAccesible);
        //     }
        // }        

        lastPosUpdated = lastPosUpd;
    }

    public void ToUpdateMap() {
        toUpdate = true;
    }

    public void UpdateMap() {
        lastPosUpdated = floorLoader.UpdateFloor(matrixLevel, lastPosUpdated);
    }

    public void DestroyMap()
    {
        if(lastPosUpdated != null)
            floorLoader.DestroyFloor((lastPosUpdated - 60) * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (toUpdate) {
            UpdateMap();
            toUpdate = false;
            toDestroy = true;
        }
         DestroyMap();


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

        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Water"))
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
