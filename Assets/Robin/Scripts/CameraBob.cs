using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{

    public float walkingBobbingSpeed;
    public float bobbingAmountY;
    public float bobbingAmountX;
    public float bobbingAmountZ;

    float defaultPosY;
    float defaultPosX;
    float defaultPosZ;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        //defaultPosY = transform.localPosition.y;
        //defaultPosX = transform.localPosition.x;
        //defaultPosZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * walkingBobbingSpeed;
        //transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmountY, transform.localPosition.z);
        //transform.localPosition = new Vector3(defaultPosX + Mathf.Sin(timer) * bobbingAmountX, transform.localPosition.y, transform.localPosition.z);
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, defaultPosZ + Mathf.Sin(timer) * bobbingAmountZ);

        Quaternion qY = Quaternion.Euler(new Vector3(0, 0 + Mathf.Sin(timer) * bobbingAmountY, 0));
        Quaternion qX = Quaternion.Euler(new Vector3(0 + Mathf.Sin(timer) * bobbingAmountX, 0, 0));
        Quaternion qZ = Quaternion.Euler(new Vector3(0, 0 , 0 + Mathf.Sin(timer) * bobbingAmountY));

        transform.rotation = qY;
        transform.rotation = qX;
        transform.rotation = qZ;
    }
}
