using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{ 
    public float health = 5;

     public void TakeDamage(float damage)
    {
        health -= damage;
        Destroy(gameObject);
    }
}
