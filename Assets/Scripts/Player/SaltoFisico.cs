using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoFisico : MonoBehaviour {


    private enum directions { left, right, up, down };

    private enum states { idle, jumping, falling };

    private states state = states.idle;
    public float jump_size = 1.5f;


    private directions direction = directions.up;


    private Vector3 dest_pos;

	private float alt_0;


    // Use this for initialization
    void Start()
    {
		alt_0 = transform.position.y;
    }

    Vector3 calculateJumpVel()
    {
        var rigid = GetComponent<Rigidbody>();

        //Vector3 p = target.position;

        Vector3 p = gameObject.transform.position + new Vector3(0.0f, 0.0f, jump_size);

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = 25 * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        //rigid.velocity = finalVelocity;

        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
        return finalVelocity;
    }

    void jump(directions d)
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        state = states.jumping;

        dest_pos = gameObject.transform.position + new Vector3(0.0f, 0.0f, jump_size);

        Vector3 new_velocity = new Vector3(0, Physics.gravity.magnitude, jump_size);
        new_velocity = calculateJumpVel();
        GetComponent<Rigidbody>().velocity = new_velocity;
        //GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, Physics.gravity.magnitude, jump_size), ForceMode.Impulse);
    }



    // Update is called once per frame
    void Update()
    {

        if (state == states.jumping || state == states.falling)
        {
            if ( transform.position.y == alt_0){
				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
				state = states.idle;
			}
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {

                jump(directions.left);
                //gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                jump(directions.right);
                //gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                jump(directions.up);
                //gameObject.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                jump(directions.down);
                //gameObject.transform.Translate(0.0f, 0.0f, -speed * Time.deltaTime);
            }
        }


    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Objeto:" + other.gameObject);
        Debug.Log("TAG: " + other.gameObject.transform.tag);
        Debug.Log("Player Position: " + gameObject.transform.position);
        if (other.gameObject.transform.tag == "Ground")
        {
            //transform.position = dest_pos;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            state = states.idle;
        }
    }
}
