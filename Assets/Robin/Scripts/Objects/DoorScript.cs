using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float angleOpen;
    public float speed;
    bool open;

    Quaternion norRot;
    Quaternion toRot;

    void Start()
    {
        norRot = transform.localRotation;
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
