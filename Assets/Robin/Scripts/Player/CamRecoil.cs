using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    [Header("Hipfire")]
    public Vector3 recoilRotation = new Vector3(2f, 2f, 2f);

    private Vector3 currentRotation;
    private Vector3 rot;

    public void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(rot);
    }

    public void Fire()
    {
        currentRotation += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }        
    }
}
