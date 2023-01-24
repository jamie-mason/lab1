using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public WheelScript[] wheels;
    [Header("Car Specs")]
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;
    [SerializeField] private float speed;

    [Header("Inputs")]
    public float steerInput;

    private float steeringAngleLeft;
    private float steeringAngleRight;



    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        steerInput = Input.GetAxis("Horizontal");

        

        if (steerInput > 0) { // is truning right
            steeringAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            steeringAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if(steerInput < 0){ //is turning left
            steeringAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            steeringAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else
        {
            steeringAngleLeft = 0;
            steeringAngleRight = 0;
        }
        foreach(WheelScript w in wheels)
        {
            if (w.wheelFrontLeft)
                w.steerAngle = steeringAngleLeft;
            if (w.wheelFrontRight)
            {
                w.steerAngle = steeringAngleRight;
            }
        }
    }
}
