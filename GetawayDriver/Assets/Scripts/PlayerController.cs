using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;

    private Vector3 startTouchPosition;
    private Vector3 endTouchPosition;

    private float lane;

    private bool moveOnX = false;

    private CameraController cameraController;

    public float zSpeed;
    public float xSpeed;

    public float leftLane = -4f;
    public float centerLane = 0f;
    public float rightLane = 4f;

    public AudioSource xMoveSFX;

    private void Start()
    {
        cameraController = GameManager.instance.levelCamera.GetComponent<CameraController>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (zSpeed >= 10)
        {
            zSpeed += .001f;
        }
        if (xSpeed != 0 && zSpeed !=0)
        {
            //Get input from the player
            HandleInput(); 
        }

        //move on the z axis
        Move();

        //if we have recieved input to move on x axis
        if(moveOnX == true)
        {
            xMoveSFX.Play();

            //change lanes
            MoveX(lane);
        }
    }

    void HandleInput()
    {
        //if player puts their finger on the screen
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button Press: True");

            //save the touch start position
            startTouchPosition = Input.mousePosition;
        }

        //if the player releases their finger from the screen
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Button Press: False");
            //save the touch end position
            endTouchPosition = Input.mousePosition;

            //if the end position on x is less than the start position, swipe left
            if(endTouchPosition.x < startTouchPosition.x)
            {
                //if we are currently in the right lane
                if(transform.position.x == rightLane)
                {
                    Debug.Log("Move Left to Center Lane");
                    //change our lane to the center lane
                    lane = centerLane;

                    //move on x axis
                    moveOnX = true;
                }
                //if we are currently in the center lane
                else if(transform.position.x == centerLane)
                {
                    Debug.Log("Move Left to Left Lane");

                    //change our lane to the left lane
                    lane = leftLane;

                    //move on x axis
                    moveOnX = true;
                }
                else
                {
                    //the player cannot move left anymore
                }
            }

            //swipe right
            if(endTouchPosition.x > startTouchPosition.x)
            {
                //if we are currently in the right lane
                if (transform.position.x == leftLane)
                {
                    Debug.Log("Move Right to Center Lane");

                    //change our lane to the center lane
                    lane = centerLane;

                    //move on x axis
                    moveOnX = true;
                }
                //if we are currently in the center lane
                else if (transform.position.x == centerLane)
                {
                    Debug.Log("Move Right to Right Lane");

                    //change our lane to the left lane
                    lane = rightLane;

                    //move on x axis
                    moveOnX = true;
                }
                else
                {
                    //the player cannot move right anymore
                }
            }
        }
    }

    void Move()
    {
        //add the speed to the forward direction over time
        Vector3 myPos = transform.position;
        myPos.z += zSpeed * Time.deltaTime;
        transform.position = myPos;
    }

    void MoveX(float moveTo)
    {
        

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveTo, transform.position.y, transform.position.z), xSpeed * Time.deltaTime);

        //cameraController.MoveOnX(moveTo);

        if(transform.position.x == moveTo)
        {
            moveOnX = false;
        }
    }
}


