using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float angleOpen;
    public float speed;
    public bool open;

    public Quaternion norRot;
    public Quaternion toRot;

    void Start()
    {
        norRot = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
    }

    void Update()
    {
        if(open)
        {
            toRot = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + angleOpen, transform.rotation.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, speed * Time.deltaTime);
        }
        else if(!open)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, norRot, speed * Time.deltaTime);
        }
    }

    public void DoorActivate()
    {
        open = !open;
    }
}
