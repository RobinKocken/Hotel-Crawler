using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonFlyAI : MonoBehaviour
{
    NavMeshAgent navDragon;
    Rigidbody rb;

    public SphereCollider colExp;

    public Transform player;
    public FPSController fps;

    public float rotSpeed;

    public ParticleSystem gore;
        
    public int damage;
    public float maxRadius;
    public float frequency;
    public float expand;

    public bool seePlayerDirect;
    bool goToPlayer;
    bool attack; 

    RaycastHit hit;
    float  disPlayer;
    Vector3 dirToPlayer;

    float startTime;

    void Awake()
    {
        navDragon = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        AttackPlayer();
        FollowPlayer();
        DragonRay();
    }

    void FollowPlayer()
    {
        if(!seePlayerDirect && !attack|| seePlayerDirect && disPlayer > 20 && !attack)
        {
            navDragon.SetDestination(transform.position);
        }

        if(seePlayerDirect && !attack || seePlayerDirect && disPlayer < 19 && !attack)
        {
            navDragon.SetDestination(player.position);
            navDragon.speed = 1;
            navDragon.stoppingDistance = 3;

            Vector3 lookrotation = navDragon.steeringTarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), rotSpeed * Time.deltaTime);
        }
        
    }

    void AttackPlayer()
    {
        if(seePlayerDirect && disPlayer < 10) attack = true;
        else attack = false;

        if(attack)
        {
            navDragon.SetDestination(player.position);
            navDragon.speed = 3;
            navDragon.stoppingDistance = 1;

            Vector3 lookrotation = navDragon.steeringTarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), rotSpeed * Time.deltaTime);
        }

        if(attack && disPlayer < 2)
        {
            navDragon.ResetPath();
            if(colExp.radius < maxRadius && Time.time - startTime > frequency)
            {
                colExp.radius += expand;
            }
        }
    }

    void DragonRay()
    {
        disPlayer = Vector3.Distance(transform.position, player.position);

        dirToPlayer = (player.position - transform.position).normalized;

        Debug.DrawRay(transform.position, dirToPlayer * disPlayer, Color.red);
        if(Physics.Raycast(transform.position, dirToPlayer, out hit, disPlayer))
        {
            if(hit.transform.tag == "Player") seePlayerDirect = true;
            else seePlayerDirect = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            fps.playerHealth -= damage;
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        var particle = Instantiate(gore, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 1f);


    }

}
