using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    private float velocity = 10;

    private float tamFloor = 1.5f;

    void Start()
    {   
        Debug.Log("Enemigo Cabrón es par?: " +  (gameObject.transform.position.y / 1.5f % 2 == 0) ) ;
        if ( ( gameObject.transform.position.z / 1.5f ) % 2 == 0 ){
            velocity *= -1;
            gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
        }
        else{
            gameObject.transform.Rotate(0.0f, -90.0f, 0.0f);
        }   
        
    }

    void Update()
    {
        
        //gameObject.transform.position += new Vector3(Time.deltaTime * velocity, 0, 0);
        float new_x = gameObject.transform.position.x + Time.deltaTime*velocity ;
        if (new_x < -45) new_x += 50; 
        if (new_x > 30) new_x -= 50;
        gameObject.transform.position = new Vector3(new_x, gameObject.transform.position.y, gameObject.transform.position.z);

        //Tiles de 1.5 van de -30 a 20 
    }
}
