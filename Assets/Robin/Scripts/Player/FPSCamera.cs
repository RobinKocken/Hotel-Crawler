using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public FPSController fps;
    public Transform orientation;

    public float rayDistance;
    public LayerMask doorLayer;
    RaycastHit hit;

    float xRotation;
    float yRotation;
    public float mouseSens;

    public float walkingBobbingSpeed;
    public float bobbingAmount;

    float defaultPosY;
    float timer;

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        Camera();
        CameraBob();
        DoorInteract();
    }

    void DoorInteract()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, doorLayer))
        {
            if(Input.GetButtonDown("E"))
            {
                hit.transform.GetComponent<DoorScript>().DoorActivate();
            }
            
        }

    }

    void CameraBob()
    {
        if(Mathf.Abs(fps.moveX) > 0.1f || Mathf.Abs(fps.moveZ) > 0.1f)
        {
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }

    void Camera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation += -mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
