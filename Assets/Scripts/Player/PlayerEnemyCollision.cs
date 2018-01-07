using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour {

    private float TimeToReset = 0;
    private GameController gameController;
    public GameObject waterSplash;
    Color origColor;

    private AudioController audioController;

    // Use this for initialization
    void Start () {
        origColor = gameObject.GetComponentInChildren<MeshRenderer>().material.color;
        gameController = GameObject.Find("Controllers").GetComponent<GameController>();
        audioController = GameObject.Find("Music").GetComponent<AudioController>();
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
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Vector3 dir = other.transform.position - transform.position;
            rb.velocity = -dir*5;

            MeshCollider mc = gameObject.GetComponent<MeshCollider>();
            mc.isTrigger = false;
            
            other.gameObject.GetComponent<Enemy>().setVelocity(0);
            Rigidbody rb_other = Utils.GetComponentAddIfNotExists<Rigidbody>(gameObject);
            rb_other.mass = 400;
            Vector3 dir_other = dir;
            dir.Scale(new Vector3(1, 0, 1));
            rb_other.useGravity = false;
            rb_other.velocity = dir * 5;

            audioController.CarCrash();
            gameController.endGame(true);
        }
        else if ( other.transform.tag == "Water")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            TimeToReset = 3.0f;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Instantiate(waterSplash, transform.position, Quaternion.identity);
            gameController.endGame();
        }
    }
}
