using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVisibility : MonoBehaviour
{
    public FPSCamera cam;

    void Awake()
    {
        cam = GetComponent<FPSCamera>();
    }

    void Start()
    {
        cam.uiActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MouseMode(bool mouseSwitch)
    {
        if(mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cam.uiActive = false;
        }
        else if(!mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            cam.uiActive = true;
        }
    }
}
