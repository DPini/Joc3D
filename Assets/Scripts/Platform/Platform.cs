using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private float speed = 5.0f;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update () {
        float new_x = gameObject.transform.position.x + Time.deltaTime * speed;
        if (new_x < -40) new_x += 70;
        if (new_x > 30) new_x -= 50;
        gameObject.transform.position = new Vector3(new_x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
