using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private float speed;
    private bool isSinking = false;

    // 1 -> right
    // -1 -> left
    public float direction;

    // Use this for initialization
    void Start()
    {

    }

    public void SetDirection(int toDirection)
    {
        direction = toDirection;
        if (toDirection > 0)
            gameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public float GetDirection()
    {
        return direction;
    }

    public void SetSpeed(float platformSpeed)
    {
        speed = platformSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SinkPlatform() {
        gameObject.transform.position -= new Vector3 (0.0f, 0.05f, 0.0f);
    }

    public void UnSinkPlatform()
    {
        gameObject.transform.position += new Vector3(0.0f, 0.05f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
        float new_x = gameObject.transform.position.x + Time.deltaTime * speed * direction ;
        if (new_x < -20) new_x = 20;
        if (new_x > 20) new_x = -20;
        gameObject.transform.position = new Vector3(new_x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
