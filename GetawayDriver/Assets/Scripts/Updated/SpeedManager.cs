using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] private float baseSpeed;

    [SerializeField] private ScoreManager scoreManager;

    private float currentSpeed;
    private float speedMultiplier;


    private void Update()
    {
        
        currentSpeed = baseSpeed * speedMultiplier;
    }

    public void UpdateMultiplier()
    {
        speedMultiplier += currentSpeed * Time.deltaTime;
    }

    public void ResetMultiplier()
    {
        speedMultiplier = 1;
    }

    public void ResetSpeed()
    {
        currentSpeed = baseSpeed;
    }

}
