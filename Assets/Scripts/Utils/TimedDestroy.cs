using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

    public float TimeToDestroy = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TimeToDestroy -= Time.deltaTime;
        if ( TimeToDestroy < 0)
        {
            Destroy(transform.gameObject);
        }
	}
}
