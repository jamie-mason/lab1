using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float acceleration;
    public float breakingForce;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed;


    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private bool isMovingForward;
    private bool isMovingBackward;

    void turnLeft()
    {
        rb.transform.RotateAround(rb.transform.position,rb.transform.up, rotationSpeed*-1f*Time.deltaTime);
    }
    void turnRight()
    {
        rb.transform.RotateAround(rb.transform.position, rb.transform.up, rotationSpeed*1f * Time.deltaTime);
    }
    private void Start()
    {

    }

    private void Update()
    {
         float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(vertical, 0f, 0f).normalized;
        currentAcceleration = acceleration * Time.fixedDeltaTime;
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.position += -transform.forward * Time.deltaTime * currentAcceleration;
            isMovingForward = true;
            isMovingBackward = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.transform.position += transform.forward * Time.deltaTime *currentAcceleration;
            isMovingBackward = true;
            isMovingForward = false;
        }
        else
        {
            isMovingForward = false;
            isMovingBackward = false;
        }
        if (Input.GetKey(KeyCode.A)){
            turnLeft();

        }
        if (Input.GetKey(KeyCode.D))
        {
            turnRight();
        }
    }
        private void FixedUpdate()
    {
       
    }
}
