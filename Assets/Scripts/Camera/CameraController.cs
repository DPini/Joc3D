using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private float offset;

    void Start()
    {
        offset = player.transform.position.z - transform.position.z;
    }

    void LateUpdate()
    {
        float cameraOffset = (player.transform.position.z - transform.position.z);
        Debug.Log("player: " + player.transform.position.z + "  - camera : " + transform.position.z);
        Debug.Log("offset: " + offset + " - "+ cameraOffset);
        float cameraSpeed = System.Convert.ToSingle(System.Math.Log(System.Math.Abs(cameraOffset/ offset)))/4.0f;
        if (cameraSpeed < 0.01f) cameraSpeed = 0.01f;
        Debug.Log("speed: " + cameraSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cameraSpeed);
    }
}
