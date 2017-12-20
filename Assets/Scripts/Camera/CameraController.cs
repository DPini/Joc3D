using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public int test;

    private float offset;

    void Start()
    {
        offset = player.transform.position.z - transform.position.z;
    }

    void LateUpdate()
    {
        float cameraOffset = (player.transform.position.z - transform.position.z);
        float cameraSpeed = System.Convert.ToSingle(System.Math.Log(System.Math.Abs(cameraOffset/ offset)))/8.0f;
        if (cameraSpeed < 0.01f) {
            if (test == 1) cameraSpeed = 0.0f;
            else cameraSpeed = 0.01f;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cameraSpeed);
    }
}
