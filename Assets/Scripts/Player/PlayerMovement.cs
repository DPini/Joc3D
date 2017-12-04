using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    private enum directions { left, right , up, down };

    private enum states { idle, jumping, falling };

    private states state = states.idle;

    public float speed = 5.0f;

    public float jump_dist = 1.5f;
    //public float jump_height = 5.0f;
    public float jump_speed = 1.0f;

    public float jump_angle = 45.0f;

    //public float gravity = 9.81f;
    public float gravity = 40.0f;

    public float jump_player_scale = 0.8f;

    private directions direction = directions.up;

    //private float jump_time;
    //private float jump_time_count;
    private Vector3 velocity;
    private Vector3 dest_pos;
    private float angular_velocity;
    private float player_scale_step;
    


    // Use this for initialization
    void Start () {
		
	}

    Vector3 direction_vector(directions d){

        Vector3 res;
        
        switch (d)
        {
            case directions.up:
                res = Vector3.forward;
                break;
            case directions.down:
                res = Vector3.back;
                break;
            case directions.left:
                res = Vector3.left;
                break;
            case directions.right:
                res = Vector3.right;
                break;
            default:
                res = Vector3.zero;
                break;
        }

        return res;

    }

    float calc_jump_rotation(directions d)
    {
        return Vector3.SignedAngle(direction_vector(direction), direction_vector(d), Vector3.up );
    }

    void jump(directions d){

        // Based on: https://vilbeyli.github.io/Simple-Trajectory-Motion-Example-Unity3D/

        //jump_time = 0;
        //current_jump = 0;

        dest_pos = transform.position + direction_vector(d)*jump_dist;        

        float Vi = Mathf.Sqrt(jump_dist * gravity / (Mathf.Sin(Mathf.Deg2Rad * jump_angle * 2)));
        Debug.Log("Vi = " + Vi);
        float Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * jump_angle);
        float Vx = Vi * Mathf.Cos(Mathf.Deg2Rad * jump_angle);

        float jump_time = Vy * 2 / gravity;
        player_scale_step = ( 1.0f-jump_player_scale )/(jump_time/2);
        //jump_time_count = 0;


        angular_velocity = calc_jump_rotation(d) / jump_time;

        velocity = ( Vx * direction_vector(d) ) + new Vector3( 0.0f, Vy, 0.0f );

        // Debug.Log("##### JUMPING #####");
        // Debug.Log("Vi = " + Vi);
        // Debug.Log("Vy = " + Vx);
        // Debug.Log("Vx = " + Vy);
        // Debug.Log("Direction vector = " + direction_vector(d));
        // Debug.Log("Velocity = " + velocity);

        state = states.jumping;
        direction = d;
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;


    }
    /*
        void jump(){
            float dir;
            if (state == states.jumping) dir = 1;
            else if (state == states.falling) dir = -1;
            else dir = 0;

            //float y = Mathf.Sin(Time.deltaTime) * dir;
            float y = Time.deltaTime * dir;
            gameObject.transform.Translate(0.0f, y, 0.0f);
            current_jump += y;
            if (current_jump > jump_height) state = states.falling;
            jump_time += Time.deltaTime;

        }
     */
    void physics_update(){
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

    // void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log("Collision: " + other.transform.tag);
    //     if (other.transform.tag == "Ground"){
    //         Debug.Log("STOPPPPPPPPPPPPPPPPP");
    //         //gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //         velocity = Vector3.zero;
    //         // Place player in precise position.
    //         transform.position = dest_pos;
    //         Debug.Log("Position after jumping: " + transform.position);
    //         state = states.idle;
    //     }        
    // }

    
    void OnTriggerEnter(Collider other){
        Debug.Log("Collision: " + other.transform.tag);
        if ( state == states.jumping ){
            if (other.transform.tag == "Ground")
            {
                Debug.Log("STOPPPPPPPPPPPPPPPPP");
                //gameObject.GetComponent<Rigidbody>().isKinematic = true;
                velocity = Vector3.zero;
                angular_velocity = 0;
                transform.localScale = new Vector3(1,1,1);
                // Place player in precise position.
                transform.position = dest_pos;
                transform.rotation = Quaternion.LookRotation(direction_vector(direction));

                Debug.Log("Position after jumping: " + transform.position);
                state = states.idle;
            }
        }
    }

	
	// Update is called once per frame
	//void Update () {
    void FixedUpdate(){

        if ( state == states.jumping || state == states.falling ){
            physics_update();
        }
        else{
            if(Input.GetKey(KeyCode.LeftArrow)){
                
                jump(directions.left);
                //gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow)){
                jump(directions.right);
                //gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.UpArrow)){
                jump(directions.up);
                //gameObject.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow)){
                jump(directions.down);
                //gameObject.transform.Translate(0.0f, 0.0f, -speed * Time.deltaTime);
            }
        }


    }
}
