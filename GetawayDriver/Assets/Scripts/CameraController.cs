using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float xSpeed;
    private float zSpeed;

    [SerializeField] private PlayerController player;

    public Transform lookAtTarget;

    private void Start()
    {
        xSpeed = player.xSpeed;
        zSpeed = player.zSpeed;
    }

    private void Update()
    {

        //Follow();

        transform.LookAt(lookAtTarget);
    }

    void Follow()
    {
        zSpeed = player.zSpeed;

        Vector3 myPos = transform.position;
        myPos.z += zSpeed * Time.deltaTime;
        transform.position = myPos;
    }

    public void MoveOnX(float moveTo)
    {
        float move = 0;

        if(moveTo == player.leftLane)
        {
            move = player.leftLane;
        }
        else if(moveTo == player.centerLane)
        {
            move = player.centerLane;
        }
        else
        {
            move = player.rightLane;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(move, transform.position.y, transform.position.z), xSpeed * Time.deltaTime);
    }
}
