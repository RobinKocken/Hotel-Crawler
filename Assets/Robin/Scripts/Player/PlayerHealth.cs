using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    void Update()
    {
        if(health <= 0)
        {
            print("Dead");
        }
    }
}
