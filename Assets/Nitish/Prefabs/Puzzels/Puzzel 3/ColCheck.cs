using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColCheck : MonoBehaviour
{
    public string nameColObj;
    public bool alreadyTouched;
    public int plusGoodAns;

    public GameObject puzzel3;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == nameColObj)
        {
            print("aanraak compleet");
            plusGoodAns = 1;
            if (alreadyTouched == false)
            {
                puzzel3.GetComponent<RayCastDrag>().goodAns += plusGoodAns;
                alreadyTouched = true;
            }                
        }
        else
        {            
            puzzel3.GetComponent<RayCastDrag>().goodAns -= plusGoodAns;  
            plusGoodAns = 0;
            print("Verkeerde kleur");
        }
    }
}
