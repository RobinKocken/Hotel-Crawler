using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public FPSController fps;
    public Transform orientation;

    float xRotation;
    float yRotation;
    public float mouseSens;

    public float swayTime;
    public float swayPos;
    public float fast;

    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;

    float defaultPosY = 0;
    float timer = 0;

    void Start()
    {
        InvokeRepeating("Repeat", 1, swayTime);
    }

    void Update()
    {
        Camera();
        CameraSway();
    }

    void CameraSway()
    {
        //Vector3 newPos = new Vector3(transform.position.x, transform.position.y + swayPos, transform.position.z);

        //Vector3 lerp = Vector3.Lerp(transform.position, newPos, swayTime * Time.deltaTime);

        //transform.position = lerp;

    }

    void Repeat()
    {
        swayPos -= swayPos * 2;
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
