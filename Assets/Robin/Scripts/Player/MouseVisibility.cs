using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVisibility : MonoBehaviour
{
    public bool mouseInvisible;

    public GameObject canvas;
    public GameObject vaas;

    void Start()
    {
        mouseInvisible = true;
    }

    void Update()
    {
        //float distance = Vector3.Distance(transform.position, vaas.transform.position);
        //if (distance <= 3 && Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("huh?");
        //    canvas.SetActive(true);
        //}

        //if (canvas.activeSelf)
        //{
        //    mouseInvisible = false;
        //}
        //else
        //{
        //    mouseInvisible = true;
        //}

        if(mouseInvisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(!mouseInvisible)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true; ;
        }
    }
}
