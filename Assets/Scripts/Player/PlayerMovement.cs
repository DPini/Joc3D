﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

   

    private enum states { idle, jumping, falling, died };

    private states state = states.idle;

    private bool godMode;

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
    private float platformSpeed;
    private float desv_compensation;

    private PlatformController platformController;

    private AudioController audioController;

    // Use this for initialization
    void Start () {
        platformController = GameObject.Find("Controllers").GetComponent<PlatformController>();

        audioController = GameObject.Find("Music").GetComponent<AudioController>();
        godMode = false;


    }

    public void killPlayer()
    {
        state = states.died;
    }

    public void setGodMode(bool b)
    {
        godMode = b;
    }

    
    private void Update()
    { 
        if (inPlatform) { 
            float new_x = gameObject.transform.position.x + Time.deltaTime * platformSpeed * platformDirection;
            float new_y = gameObject.transform.position.y;
            if (new_x < -9.0f)
            {
                dest_pos = transform.position;
                new_y -= 0.2f;
                state = states.jumping;
                inPlatform = false;
            }
            if (new_x > 5.0f)
            {
                dest_pos = transform.position;
                new_y -= 0.2f;
                state = states.jumping;
                inPlatform = false;
            }
            gameObject.transform.position = new Vector3(new_x, new_y, gameObject.transform.position.z);
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

    public void Jump(Directions d, int nextZone)
    {

        // Based on: https://vilbeyli.github.io/Simple-Trajectory-Motion-Example-Unity3D/

        if (state == states.jumping || state == states.falling)
        {
            inPlatform = false;
        }
        else {

            platformController.UnSinkPlatform();
            audioController.Jump();

            dest_pos = transform.position + direction_vector(d) * jump_dist;
            if (nextZone == 1) dest_pos = new Vector3(dest_pos.x, 0.3f, dest_pos.z);
            else dest_pos = new Vector3(dest_pos.x, 0.4f, dest_pos.z);
            float desv = CalcDesviation(dest_pos.x);
            //Debug.Log("Desviación: " + desv);
            dest_pos.x += desv;

            float Vi = Mathf.Sqrt(jump_dist * gravity / (Mathf.Sin(Mathf.Deg2Rad * jump_angle * 2)));
            float Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * jump_angle);
            float Vx = Vi * Mathf.Cos(Mathf.Deg2Rad * jump_angle);

            float jump_time = Vy * 2 / gravity;
            player_scale_step = (1.0f - jump_player_scale) / (jump_time / 2);


            angular_velocity = calc_jump_rotation(d) / jump_time;
            desv_compensation = desv / jump_time;


            velocity = (Vx * direction_vector(d)) + new Vector3(0.0f, Vy, 0.0f);


            state = states.jumping;
            inPlatform = false;
            direction = d;
        }

    }

    private float CalcDesviation( float position ) { 
        Debug.Log("dest x pos: " + position);
        Debug.Log("Modulo :" + position % 1.5f);
        float desviation = (position % 1.5f) + 1.5f % 1.5f;
        if (desviation != 0)
        {
            if (Mathf.Abs(desviation) < (1.5f / 2.0f))
            {
                Debug.Log("Desviación derecha: " + -desviation);
                return -desviation;
            }
            else
            {
                Debug.Log("Desviación izquierda: " + (1.5f - Mathf.Abs(desviation)));
                float finalDesviation = 1.5f - Mathf.Abs(desviation);
                if (position < 0) finalDesviation = -finalDesviation;
                return finalDesviation;
            }
        }
        //Debug.Log("Desviación: " + desviation);
        return 0.0f;
    }
    
    public void Physics_update() { 
        if ( state == states.jumping ) { 
            //jump_time_count += Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
            /*
            if(direction == Directions.up || direction == Directions.down)
            {
                
                float desviation = CalcDesviation();
                if (desviation != 0.0f)
                {
                    Debug.Log("Desviation :" + desviation);
                    if (Mathf.Abs(desviation) < 0.4f) velocity.x += desviation;
                    else if (desviation < 0.0f)
                    {
                        Debug.Log("Moving left with Desviation: " + desviation);
                        velocity.x -= 0.4f;
                    }
                    else
                    {
                        Debug.Log("Moving right with Desviation: " + desviation);
                        velocity.x += 0.4f;
                    }
                }
            }
            */
            
            gameObject.transform.position += velocity * Time.deltaTime;
            gameObject.transform.Rotate(0, angular_velocity * Time.deltaTime, 0);
            gameObject.transform.position += new Vector3 (desv_compensation*Time.deltaTime,0,0);
            transform.localScale += new Vector3(
                0.0f,
                ( transform.localScale.y <= jump_player_scale ? player_scale_step : -player_scale_step ) * Time.deltaTime,
                0.0f
            );
            
        }
    }
    
    void OnTriggerEnter(Collider other){
        if ( state == states.jumping ){
            if (other.transform.tag.Contains("Ground") || godMode && other.transform.tag.Contains("Water") )
            {
                velocity = Vector3.zero;
                angular_velocity = 0;
                transform.localScale = new Vector3(1,1,1);
                // Place player in precise position.
                transform.position = dest_pos;
                //Debug.Log("Aterrizando en: " + transform.position.x);
                transform.rotation = Quaternion.LookRotation(direction_vector(direction));

                state = states.idle;

                if (other.transform.tag.Contains("Platform")) {
                    inPlatform = true;
                    Platform platform = other.gameObject.GetComponent<Platform>();
                    platformDirection = platform.GetDirection();
                    platformSpeed = platform.GetSpeed();

                    platformController.SinkPlatform(platform);
                }


            }
        }
    }
}
