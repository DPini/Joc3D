using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

   

    private enum states { idle, jumping, falling };

    private states state = states.idle;

    public float speed = 5.0f;

    public float jump_dist = 1.5f;
    public float jump_speed = 1.0f;

    public float jump_angle = 45.0f;

    public float gravity = 40.0f;

    public float jump_player_scale = 0.8f;

    private Directions direction = Directions.up;

    private Vector3 velocity;
    private Vector3 dest_pos;
    private float angular_velocity;
    private float player_scale_step;

    private bool inPlatform = false;
    private float platformDirection;



    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    { 
        if (inPlatform) {
            float new_x = gameObject.transform.position.x + Time.deltaTime * 5.0f * platformDirection;
            gameObject.transform.position = new Vector3(new_x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    public static Vector3 direction_vector(Directions d){

        Vector3 res;
        
        switch (d)
        {
            case Directions.up:
                res = Vector3.forward;
                break;
            case Directions.down:
                res = Vector3.back;
                break;
            case Directions.left:
                res = Vector3.left;
                break;
            case Directions.right:
                res = Vector3.right;
                break;
            default:
                res = Vector3.zero;
                break;
        }

        return res;

    }

    float calc_jump_rotation(Directions d)
    {
        return Vector3.SignedAngle(direction_vector(direction), direction_vector(d), Vector3.up );
    }

    public void Jump(Directions d){

        // Based on: https://vilbeyli.github.io/Simple-Trajectory-Motion-Example-Unity3D/

        if (state == states.jumping || state == states.falling)
        {
            Physics_update();
        }
        else {
            dest_pos = transform.position + direction_vector(d) * jump_dist;

            float Vi = Mathf.Sqrt(jump_dist * gravity / (Mathf.Sin(Mathf.Deg2Rad * jump_angle * 2)));
            float Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * jump_angle);
            float Vx = Vi * Mathf.Cos(Mathf.Deg2Rad * jump_angle);

            float jump_time = Vy * 2 / gravity;
            player_scale_step = (1.0f - jump_player_scale) / (jump_time / 2);


            angular_velocity = calc_jump_rotation(d) / jump_time;

            velocity = (Vx * direction_vector(d)) + new Vector3(0.0f, Vy, 0.0f);


            state = states.jumping;
            inPlatform = false;
            direction = d;
        }

    }
    
    public void Physics_update(){
        if ( state == states.jumping ){
            //jump_time_count += Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
            gameObject.transform.position += velocity * Time.deltaTime;
            gameObject.transform.Rotate(0, angular_velocity * Time.deltaTime, 0);
            transform.localScale += new Vector3(
                0.0f,
                ( transform.localScale.y <= jump_player_scale ? player_scale_step : -player_scale_step ) * Time.deltaTime,
                0.0f
            );
            
        }
    }
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Collision: " + other.transform.tag);
        if ( state == states.jumping ){
            if (other.transform.tag.Contains("Ground"))
            {
                velocity = Vector3.zero;
                angular_velocity = 0;
                transform.localScale = new Vector3(1,1,1);
                // Place player in precise position.
                transform.position = dest_pos;
                transform.rotation = Quaternion.LookRotation(direction_vector(direction));

                state = states.idle;

                if (other.transform.tag.Contains("Platform")) {
                    inPlatform = true;
                    Platform platform = other.gameObject.GetComponent<Platform>();
                    platformDirection = platform.GetDirection();
                }

                
            }
        }
    }
}
