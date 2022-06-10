using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDrag : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject moveToPos;
    public Camera targetCamera;

    public Vector3 selecObj;
    public Vector3 moveVeToPos;

    public bool selectedObjSwitch;
    public bool moveObjSwitch;
    public bool freeHolder;

    public float maxDist;
    public float maxDistDraw;
    public float speed;

    public int goodAns;

    public RaycastHit hit;
    public Ray ray;

    public Vector3[] directions = new Vector3[4] {
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left
    };

    public void Start()
    {
        selectedObjSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if(hit.collider.gameObject.tag == "DraggableObject")
                {
                    selectedObject = hit.collider.gameObject;
                    selectedObjSwitch = true;
                    selecObj = selectedObject.transform.position;
                }

                for (int i = 0; i < directions.Length; i++)
                {
                    Debug.DrawRay(selecObj, directions[i] * maxDistDraw, Color.red);
                    if (Physics.Raycast(selecObj, directions[i], out hit, maxDistDraw))
                    {
                        if(hit.collider.gameObject.tag == "FreeHolders")
                        {
                            moveToPos = hit.collider.gameObject;                            
                            moveObjSwitch = true;
                            //moveVeToPos = moveToPos.transform.position;
                        }
                    }
                }
            }
        }

        if (selectedObject != null && moveToPos != null)
        {
            //added line
            Vector3 direction = moveToPos.transform.position - selectedObject.transform.position;
            //
            //selectedObject.transform.position = moveToPos.transform.position;
            //added line
            selectedObject.transform.position += direction * speed * Time.deltaTime;
            print("Hai");
        }

        if (Input.GetButtonUp("Fire1"))
        {
            selectedObject = null;
            selectedObjSwitch = false;
            moveToPos = null;
            moveObjSwitch = false;
        }

        if(goodAns == 8)
        {
            print("You won!");
        }
    }
}
