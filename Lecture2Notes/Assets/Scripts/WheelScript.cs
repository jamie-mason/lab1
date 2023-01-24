using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelRearLeft;
    public bool wheelRearRight;

    private Rigidbody rb;

    [Header("Suspension")]//creates a header in the instance menu.
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springVeloctiy;
    private float springForce;
    private float damperForce;


    private Vector3 suspensionForce;
    private Vector3 wheelVelLS; // Local space
    private float Fx;  //Force on x axis
    private float Fy;  //Force on y axis
    private float wheelAngle;
    
    [Header("Wheel")]//creates a header in the instance menu.
    public float wheelRadius;
    public float steerAngle;
    public float steerTime;
    private void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    private void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLength * wheelRadius) , Color.green);
    }
    private void FixedUpdate() 
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVeloctiy = (lastLength - springLength) / Time.fixedDeltaTime /*Time.fixedDetaTime is the delta time of the physics value*/ ;
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVeloctiy;

            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            Fx = Input.GetAxis("Vertical") * springForce;
            Fy = wheelVelLS.x * springForce;
            rb.AddForceAtPosition(suspensionForce + (Fx * transform.forward) + (Fy * -transform.right), hit.point);
        }
    }
}
