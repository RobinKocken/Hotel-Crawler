using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    [SerializeField] private LayerMask layerGrab;

    public RaycastHit hit;
    public Ray ray;
    public Camera playerCam;
    public float maxDis;
    public float disToPoint;

    public Transform pickPos;
    public Rigidbody currentObj;

    public Vector3 directToPoint;

    public void Update()
    {
        if (Input.GetButtonDown("PickUp"))
        {
            if (currentObj)
            {
                currentObj.useGravity = true;
                currentObj = null;
                return;
            }

            ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(transform.position, transform.forward, out hit, maxDis, layerGrab))
            {
                currentObj = hit.rigidbody;
                currentObj.useGravity = false;
            }
        }
    }

    public void FixedUpdate()
    {
        if (currentObj)
        {
            directToPoint = pickPos.position - currentObj.transform.position;
            disToPoint = directToPoint.magnitude;

            currentObj.velocity = directToPoint * 12f * disToPoint;
        }
    }
}
