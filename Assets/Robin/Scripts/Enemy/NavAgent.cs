using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    public Transform targetPos;

    NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>(); 
    }

    void Start()
    {
        
    }


    void Update()
    {
        navAgent.destination = targetPos.position;
    }
}
