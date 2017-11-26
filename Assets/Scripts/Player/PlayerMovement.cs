using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    private enum directions { left, right , up, down };

    private enum states { idle, jumping, falling };

    private states state = states.idle;

    public float speed = 5.0f;

    public float jump_size = 5.0f;
    public float jump_height = 5.0f;
    public float jump_speed = 1.0f;

    private directions direction = directions.up;

    private float jump_time;
    private float current_jump;
    


    // Use this for initialization
    void Start () {
		
	}

    void jump(directions d){
        state = states.jumping;
        jump_time = 0;
        current_jump = 0;
    }

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

	
	// Update is called once per frame
	void Update () {

        if ( state == states.jumping || state == states.falling ){
            jump();
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
