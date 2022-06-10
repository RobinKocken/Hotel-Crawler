using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideLine : MonoBehaviour
{

    public float forceAmount = 500;

    public LayerMask dragAbleMask;
    Rigidbody selectedRigidbody;
    public Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    public RaycastHit hitInfo;
    public Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            //kijk voor rigidbody zo ja pak het
            selectedRigidbody = GetRigidbodyFromMouseClick();
            Cursor.visible = false;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Cursor.visible = true;
        }

        if (Input.GetButtonUp("Fire1") && selectedRigidbody)
        {
            //laat los als er een rigidbody is 
            selectedRigidbody = null;
        }
    }
    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        hitInfo = new RaycastHit();
        ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo, dragAbleMask);
        if (hit)
        {           
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
        return null;
    }
}

