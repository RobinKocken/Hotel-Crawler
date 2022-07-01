using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public FPSController fps;
    public Transform orientation;
    public MouseVisibility mouseVis;
    public UIManagerScript uiMan;

    [SerializeField] public KeyCode interactKey;
    [SerializeField] public KeyCode inventoryKey;
    [Header("Inventory")]
    public InventoryObject inventory;
    public ItemObject ammoType;

    public GameObject overlayCanvas;
    public GameObject menu;
    public GameObject inven;
    public GameObject user;
    public WeaponSway weaponSway;

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

    public GameObject eToInteract;

    void Awake()
    {
        mouseVis = GetComponent<MouseVisibility>();
    }

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        Camera();
        CameraBob();
        Interact();
    }

  

    void Interact()
    {
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            if(hit.transform.tag == "Interactable" || hit.transform.tag == "Door" || hit.transform.tag == "Scene")
            {
                eToInteract.SetActive(true);
            }

            if(Input.GetKeyDown(interactKey) && hit.transform.tag == "Door")
            {
                hit.transform.GetComponent<DoorScript>().DoorActivate();
            }
            else if(Input.GetKeyDown(interactKey) && hit.transform.tag == "Scene")
            {
                hit.transform.GetComponent<SceneScript>().LoadLevelOne();
            }

            if(Input.GetKeyDown(interactKey) && hit.transform.GetComponent<SceneScript>() && hit.transform.tag != "Scene")
            {
                hit.transform.GetComponent<SceneScript>().WinScreen();
            }

            var item = hit.collider.gameObject.GetComponent<Item>();
            if(Input.GetKeyDown(interactKey))
            {
                if(item)
                {
                    inventory.AddItem(item.item, 1);
                    if (item.item == ammoType)
                    {
                        inventory.AddItem(item.item, ammoType.itemValue);
                        inventory.GetAmount(ammoType).ToString();

                    }
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        else
        {
            eToInteract.SetActive(false);
        }

        if(Input.GetKeyDown(inventoryKey))
        {
            uiActive = !uiActive;
            weaponSway.uiActive = uiActive;

            overlayCanvas.SetActive(uiActive);
            menu.SetActive(false);
            inven.SetActive(uiActive);
            user.SetActive(!uiActive);

            uiMan.ChangeInv();

            mouseVis.MouseMode(!uiActive);
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
