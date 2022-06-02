using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoobaAI : MonoBehaviour
{
    NavMeshAgent navBooba;
    Rigidbody rb;
    Transform player;
    FPSController fpsController;

    public float boobaRotSpeed;

    public float sphereRange;
    public LayerMask layerPlayer;
    RaycastHit hit;

    Vector3 playerPos;
    public bool inSphereRange;
    public bool seePlayer;

    public float disPlayer;

    bool canAttack;
    bool isAttacking;
    bool hasAttacked;

    public float boobaJumpForce;
    public float boobaJumpForwardForce;

    public int damage;

    float startTime;
    public float waitForSec;

    void Awake()
    {
        navBooba = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        FieldOfView();
        FollowPlayer();
        AttackPlayer();
    }

    void FollowPlayer()
    {
        if(!seePlayer && !isAttacking || disPlayer > 6 && !isAttacking)
        {
            navBooba.SetDestination(playerPos);
            navBooba.stoppingDistance = 5;
            Vector3 lookrotation = navBooba.steeringTarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), boobaRotSpeed * Time.deltaTime);
        }
    }

    void AttackPlayer()
    {
        if(seePlayer && disPlayer < 6 && !hasAttacked)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if(hasAttacked && !isAttacking)
        {
            if(hasAttacked && Time.time - startTime > waitForSec)
            {
                hasAttacked = false;
                print("hasAttacked = false");
            }
        }

        if(canAttack)
        {
            if(disPlayer > 2  && !isAttacking)
            {
                print("DisPlay > 2");
                navBooba.SetDestination(playerPos);
                navBooba.stoppingDistance = 2;
                Vector3 lookrotation = navBooba.steeringTarget - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), boobaRotSpeed * Time.deltaTime);
            }

            if(disPlayer < 2 && !hasAttacked)
            {
                print("DisPlay < 2, Attack");
                isAttacking = true;

                navBooba.enabled = false;

                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * boobaJumpForce, ForceMode.Impulse);
                rb.AddForce(transform.forward * boobaJumpForwardForce, ForceMode.Impulse);

                startTime = Time.time;
                hasAttacked = true;
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

                fpsController = range[0].GetComponent<FPSController>();
                player = range[0].transform;
            }
        }

        if(inSphereRange)
        {
            playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            disPlayer = Vector3.Distance(transform.position, playerPos);

            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float disToPlayer = Vector3.Distance(transform.position, player.position);

            Debug.DrawRay(transform.position + transform.forward * 0.3f, dirToPlayer * disToPlayer, Color.red);
            if(Physics.Raycast(transform.position + transform.forward * 0.3f, dirToPlayer, out hit, disToPlayer))
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
        if(collision.transform.tag == "Player" && isAttacking)
        {
            fpsController.playerHealth -= damage;
        }

        if(collision.transform.tag == "Ground" && isAttacking)
        {
            isAttacking = false;

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
