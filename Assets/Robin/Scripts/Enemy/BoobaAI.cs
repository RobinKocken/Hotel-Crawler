using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoobaAI : MonoBehaviour
{
    NavMeshAgent navBooba;
    Rigidbody rb;
    Transform player;
    PlayerHealth healthPlayer;

    public float boobaMovSpeed;
    public float boobaRotSpeed;
    public float boobaJumpForce;
    public float boobaJumpForwardForce;

    bool moving;
    bool jumped;

    public float sphereRange;
    public LayerMask layerPlayer;
    RaycastHit hit;

    Vector3 playerPos;
    public bool inSphereRange;
    public bool seePlayer;

    public float disPlayer;

    bool canAttack;
    bool isAttacking;
    public int damage;

    void Awake()
    {
        navBooba = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }
    public bool cool;

    Vector3 velocity;
    Vector3 prevPos;
    void Update()
    {
        FieldOfView();
        FollowPlayer();
        AttackPlayer();

        velocity = transform.InverseTransformDirection(transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;

        print(velocity.z);
    }

    void FollowPlayer()
    {
        if(!seePlayer || disPlayer > 6)
        {
            moving = true;
            navBooba.SetDestination(playerPos);
            navBooba.stoppingDistance = 5;
            Vector3 lookrotation = navBooba.steeringTarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), boobaRotSpeed * Time.deltaTime);
        }
        else
        {
            moving = false;
        }
    }

    void AttackPlayer()
    {
        if(seePlayer && !moving && disPlayer < 6)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if(canAttack)
        {
            if(disPlayer > 2)
            {
                navBooba.SetDestination(playerPos);
                navBooba.stoppingDistance = 2;
                Vector3 lookrotation = navBooba.steeringTarget - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), boobaRotSpeed * Time.deltaTime);
            }



            if(disPlayer < 2 && isAttacking)
            {
                navBooba.enabled = false;

                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * boobaJumpForce, ForceMode.Impulse);
                rb.AddForce(transform.forward * boobaJumpForwardForce, ForceMode.Impulse);

                jumped = true;
            }
        }

    }

    void FieldOfView()
    {
        if(!inSphereRange)
        {
            Collider[] range = Physics.OverlapSphere(transform.position, sphereRange, layerPlayer);

            if(range.Length != 0)
            {
                inSphereRange = true;

                healthPlayer = range[0].GetComponent<PlayerHealth>();
                player = range[0].transform;
            }
        }

        if(inSphereRange)
        {
            playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            disPlayer = Vector3.Distance(transform.position, playerPos);

            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float disToPlayer = Vector3.Distance(transform.position, player.position);

            Debug.DrawRay(transform.position, dirToPlayer * disToPlayer, Color.red);
            if(Physics.Raycast(transform.position, dirToPlayer, out hit, disToPlayer))
            {
                if(hit.transform.tag == "Player")
                {
                    seePlayer = true;
                }
                else
                {
                    seePlayer = false;
                }
            }

            if(seePlayer)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerPos - transform.position), boobaRotSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground" && jumped)
        {
            navBooba.enabled = true;

            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRange);
    }
}
