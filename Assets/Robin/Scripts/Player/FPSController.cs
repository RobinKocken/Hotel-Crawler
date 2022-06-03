using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public Transform orientation;
    public Rigidbody rb;

    public int playerHealth;

    public float speed;
    public float walkingSpeed;
    public float runningSpeed;
    public float maxSpeed;
    public float airMulti;

    public bool moving;
    public bool running;

    public bool grounded;

    public float drag;

    public bool readyJump;
    public bool jumped;
    public float jumpForce;

    public LayerMask ground;
    RaycastHit hitGround;
    GameObject groundObject;
    public float rayDistanceGround;
    public float sphereRadius;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerInput();
        Run();
        SpeedControl();
        RayGround();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Health()
    {
        if(playerHealth <= 0)
        {
            print("Dead");
        }
    }

    void PlayerInput()
    {
        if(Input.GetButton("Shift") && grounded)
        {
            running = true;
        }
        else if(Input.GetButtonUp("Shift"))
        {
            running = false;
        }

        if(Input.GetButtonDown("Jump") && grounded)
        {
            readyJump = true;
        }
    }

    void Movement()
    {
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);
        moveDir = orientation.TransformDirection(moveDir);

        if(grounded)
        {
            rb.AddForce(orientation.forward.normalized * moveZ * speed * 10, ForceMode.Force);
            rb.AddForce(orientation.right.normalized * moveX * speed * 10, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(orientation.forward.normalized * moveZ * speed * 10 * airMulti, ForceMode.Force);
            rb.AddForce(orientation.right.normalized * moveX * speed * 10 * airMulti, ForceMode.Force);
        }

        if(grounded)
        {
            rb.drag = drag;
            moving = true;
        }
        else if(!grounded)
        {
            rb.drag = 0;
            moving = true;
        }
    }

    void Run()
    {
        if(running)
        {
            speed = runningSpeed;
            maxSpeed = runningSpeed;
        }     
        else if(!running)
        {
            speed = walkingSpeed;
            maxSpeed = walkingSpeed;
        }
    }

    void Jump()
    {
        if(readyJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            readyJump = false;
            jumped = true;
        }
        else if(jumped && grounded)
        {
            jumped = false;
            readyJump = false;
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void RayGround()
    {
        if(Physics.SphereCast(transform.position, sphereRadius, -transform.up, out hitGround, rayDistanceGround, ground))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //RayGround
        Gizmos.color = Color.red;
        Debug.DrawRay(transform.position, -transform.up * rayDistanceGround);
        Gizmos.DrawWireSphere(transform.position + -transform.up * rayDistanceGround, sphereRadius);
    }
}
