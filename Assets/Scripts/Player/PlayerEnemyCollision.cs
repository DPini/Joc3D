using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour {

    private float TimeToReset = 0;
    Color origColor;

	// Use this for initialization
	void Start () {
        origColor = gameObject.GetComponentInChildren<MeshRenderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {

        if ( TimeToReset > 0 )
        {
            TimeToReset -= Time.deltaTime;

            if ( TimeToReset < 0)
            {
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = origColor;
            }

        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            TimeToReset = 3.0f;
        }
    }
}
