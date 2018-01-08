using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDecorator : MonoBehaviour {


	public GameObject[] trees;
    public GameObject[] ricks;

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


	public bool[] DecorateRow( int i , int zoneType, int level, int freeRow)
    {
        bool[] row = new bool[50];
        switch (zoneType)
		{	
			//Road
			case 1:
				break;
            //River
            case 2:
                break;
			//safe Zone
			default:
                row = DecorateGrassFloor(i, level, freeRow);

				break;
		}
        return row;
		
	}

	GameObject getRandomTreeInstance(int level, int i)
    {
        if (level == 1 && (i <= -7 || i >= 4)) return ricks[Random.Range(0, ricks.Length)];
        if (level == 2) {
            return trees[Random.Range(3, trees.Length)];
        }
		return trees[Random.Range(0, 3)];
	}


	private bool[] DecorateGrassFloor( int position, int level, int freeRow)
    {
        bool[] row = new bool[50];

        GameObject obj;
        for (int i = -12; i < 10; ++i) { 
			bool putTree = true;
			if ( !(i == -7 || i == 4) ){
                if (Mathf.Abs(position) < 2 && Mathf.Abs(i) < 2) putTree = false;
                else if (Random.Range(0, (Mathf.Abs(i) < 3 ? 150 : 100)) > 50) putTree = false;
                else if (i == freeRow) putTree = false;

            }
            
			if ( putTree ) { 
            	obj = Instantiate(getRandomTreeInstance(level, i), new Vector3(i * tamFloor, 0.5f, position * tamFloor), new Quaternion(0.0f, Mathf.PI /2, 0.0f, 0.0f)) as GameObject;
            	obj.transform.parent = gameObject.transform;
                if(level == 1)
                {
                    Vector3 rot = obj.transform.rotation.eulerAngles;
                    if (i <= -7) { 
                    
                        rot = new Vector3(rot.x, rot.y - 90, rot.z);
                        
                    }
                    else {
                           rot = new Vector3(rot.x, rot.y + 90, rot.z);
                    }
                    obj.transform.rotation = Quaternion.Euler(rot);

                }
                
            }
            row[30 + i] = putTree;
        }
        return row;
    }
        
 }
