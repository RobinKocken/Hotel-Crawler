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

    public float rayDistance;
    public float rayAngle;
    RaycastHit hot;
    public Collider colli;
    public Vector3 sizeCol;
    public Vector3 borderPos;
    

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
        //Walk();

        Test();
    }

    void EnemyState()
    {

    }

    void Attack()
    {
        
    }

    // V3 = transform.position = (new V3(x, y, z) / 2)
    void Walk()
    {

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

    public GameObject help;
    public Vector3 gds;
    public bool cool;
    void Test()
    {
        rb.drag = drag;

        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, transform.forward, out hot, rayDistance))
        {
            if(hot.transform.tag == "Test")
            {
                GameObject hello = hot.transform.gameObject;
                colli = hello.GetComponent<Collider>();

                sizeCol = colli.bounds.size;

                borderPos = hello.transform.position + (new Vector3(sizeCol.x, sizeCol.y, 0) / 2);
                Instantiate(help, borderPos, Quaternion.identity);
                cool = true;
            }
        }

        if(cool)
        {
            gds = new Vector3(borderPos.x + 1f, transform.position.y, borderPos.z);
            float disPos = Vector3.Distance(transform.position, gds);

            transform.position = Vector3.Lerp(transform.position, gds, boobaMovSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerPos - transform.position), boobaRotSpeed * Time.deltaTime);
            
            if(disPos < 0.5f)
            {
                cool = false;
            }
        }
    }

    void RaycastVision()
    {
        //Forward
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, transform.forward, out hot, rayDistance))
        {
            //if(hot.transform.tag == "Test")
            //{
            //    GameObject bla = hot.transform.gameObject;
            //    colli = bla.GetComponent<Collider>();

            //    size = colli.bounds.size;

            //    tell = bla.transform.position + (new Vector3(size.x, -size.y, 0) / 2);
            //    Instantiate(help, tell, Quaternion.identity);

            //    Debug.Log(size);
            //}
        }

        //Right
        // rayAngle / 2
        Debug.DrawRay(transform.position, Quaternion.Euler(0, rayAngle / 2, 0) * transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, Quaternion.Euler(0, rayAngle / 2, 0) * transform.forward, out hot, rayDistance))
        {

        }

        //rayAngle
        Debug.DrawRay(transform.position, Quaternion.Euler(0, rayAngle, 0) * transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, Quaternion.Euler(0, rayAngle, 0) * transform.forward, out hot, rayDistance))
        {

        }

        //rayAngle * 1.5f
        Debug.DrawRay(transform.position, Quaternion.Euler(0, rayAngle * 1.5f, 0) * transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, Quaternion.Euler(0, rayAngle * 1.5f, 0) * transform.forward, out hot, rayDistance))
        {

        }

        //Right
        Debug.DrawRay(transform.position, transform.right * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, transform.right, out hot, rayDistance))
        {

        }

        //LeftRay
        //-rayAngle / 2
        Debug.DrawRay(transform.position, Quaternion.Euler(0, -rayAngle / 2, 0) * transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, Quaternion.Euler(0, -rayAngle / 2, 0) * transform.forward, out hot, rayDistance))
        {

        }

        //-rayAngle
        Debug.DrawRay(transform.position, Quaternion.Euler(0, -rayAngle, 0) * transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, Quaternion.Euler(0, -rayAngle, 0) * transform.forward, out hot, rayDistance))
        {

        }

        //-rayAngle * 1.5f
        Debug.DrawRay(transform.position, Quaternion.Euler(0, -rayAngle * 1.5f, 0) * transform.forward * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, Quaternion.Euler(0, -rayAngle, 0) * transform.forward, out hot, rayDistance))
        {

        }

        //Left
        Debug.DrawRay(transform.position, -transform.right * rayDistance, Color.cyan);
        if(Physics.Raycast(transform.position, -transform.right, out hot, rayDistance))
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRange);
    }
}
