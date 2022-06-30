using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public FPSController fps;
    public Transform orientation;
    [SerializeField] private KeyCode interactKey;

    public float mouseY;
    public float mouseX;

    public bool uiActive;

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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 1000))
        {
            var item = hitInfo.collider.gameObject.GetComponent<Item>();
            if (Input.GetKeyDown(interactKey))
            {
                if (item)
                {
                    inventory.AddItem(item.item, 1);
                    Destroy(hitInfo.collider.gameObject);
                }
            }

        }
    }

    //Begin Inventory Script
    [Header("Inventory")]
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    //End Inventory Script
    void DoorInteract()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, doorLayer))
        {
            if(Input.GetKeyDown(interactKey) && hit.transform.tag != "Scene")
            {
                hit.transform.GetComponent<DoorScript>().DoorActivate();
            }
            else if(Input.GetKeyDown(interactKey) && hit.transform.tag == "Scene")
            {
                hit.transform.GetComponent<SceneScript>().LoadLevelOne();
            }
            
        }

    }

    void CameraBob()
    {
        if (Mathf.Abs(fps.moveX) > 0.1f || Mathf.Abs(fps.moveZ) > 0.1f)
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
        if(!uiActive)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            xRotation += -mouseY;
            yRotation += mouseX;

            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
