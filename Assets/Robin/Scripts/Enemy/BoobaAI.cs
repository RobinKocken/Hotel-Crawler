using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobaAI : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;

    public float boobaMovSpeed;
    public float boobaRotSpeed;
    public float drag;

    public float sphereRange;
    public LayerMask layerPlayer;
    RaycastHit hit;

    public Vector3 playerPos;
    public bool seePlayer;
    public bool found;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //FieldOfView();
        //WalkToPlayer();

        Walk();
    }

    void EnemyState()
    {

    }

    void Attack()
    {
        
    }

    public Collider colli;
    RaycastHit hot;
    public Vector3 size;

    void Walk()
    {
        Debug.DrawRay(transform.position, transform.forward * 10);
        if(Physics.Raycast(transform.position, transform.forward, out hot, 10))
        {
            if(hot.transform.tag == "Test")
            {
                GameObject bla = hot.transform.gameObject;
                colli = bla.GetComponent<Collider>();

                size = colli.bounds.size;

                Debug.Log(size);
                
            }
        }
    }

    void WalkToPlayer()
    {
        rb.drag = drag;

        if(seePlayer)
        {
            playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);

            transform.position = Vector3.Lerp(transform.position, playerPos, boobaMovSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerPos - transform.position), boobaRotSpeed * Time.deltaTime);
        }
    }

    void FieldOfView()
    {
        Collider[] range = Physics.OverlapSphere(transform.position, sphereRange, layerPlayer);

        if(range.Length != 0)
        {
            seePlayer = true;

            player = range[0].transform;
        }
        //else
        //{
        //    seePlayer = false;
        //    player = null;
        //}

        if(seePlayer)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float disToPlayer = Vector3.Distance(transform.position, player.position);

            Debug.DrawRay(transform.position, dirToPlayer * disToPlayer, Color.red);
            if(Physics.Raycast(transform.position, dirToPlayer, out hit, disToPlayer))
            {
                if(hit.transform.tag == "Player")
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRange);
    }
}
