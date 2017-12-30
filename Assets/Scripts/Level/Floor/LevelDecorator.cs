using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDecorator : MonoBehaviour {


	public GameObject[] trees;
    //public GameObject smallTreeInstance;
    public GameObject mediumTreeInstance;
	public GameObject bigTreeInstance;


	private float tamFloor = 1.5f;


	// Use this for initialization
	void Start () {
        // Do nothing
    }
	
	// Update is called once per frame
	void Update () {
		// Do nothing	
	}

	public bool[] DecorateRow( int i , int zoneType ){
        bool[] row = new bool[50];
        switch (zoneType)
		{	
			//Road
			case 1:
				break;

			//safe Zone
			default:
                row = DecorateGrassFloor(i);
				break;
		}
        return row;
		
	}

	GameObject getRandomTreeInstance(){
		return trees[Random.Range(0,trees.Length)];
	}

	private bool[] DecorateGrassFloor( int position ){
        bool[] row = new bool[50];

        GameObject obj;
        for (int i = -30; i < 20; ++i) { 
			bool putTree = true;
			if ( !(i == -10 || i == 10) ){
				if ( Random.Range(0, ( Mathf.Abs(i) < 3 ? 150 : 100 )) > 50 ) putTree = false;
			}
            
			if ( putTree ) { 
            	obj = Instantiate(getRandomTreeInstance(), new Vector3(i * tamFloor, 0.5f, position * tamFloor), new Quaternion(0.0f, Mathf.PI / 2, 0.0f, 0.0f)) as GameObject;
            	obj.transform.parent = gameObject.transform;
			}
            row[30 + i] = putTree;
        }
        return row;
    }
        
 }
