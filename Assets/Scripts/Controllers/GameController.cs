using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //Floor prefabs
    public GameObject safeFloorInstance;
    public GameObject roadFloorInstance;

    private LevelController levelController;
	// Use this for initialization
	void Start () {
        levelController = new LevelController();
        levelController.Start();
        levelController.LoadPrefabs(safeFloorInstance, roadFloorInstance);
        levelController.InitMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
