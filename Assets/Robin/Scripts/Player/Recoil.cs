using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public Transform recoilBeginPos;
    public Transform recoilEndPos;
    public float recoilSpeed;

    [Header("Cam Recoil")]
    public Camera fpsCam;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        this.transform.position = Vector3.Lerp(recoilBeginPos.position, recoilEndPos.position, recoilSpeed * Time.deltaTime );
    }
}
