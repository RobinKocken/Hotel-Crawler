using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Ammo")]
    public int currentAmmo;
    public int magSize;
    [Tooltip("in RPM")]public float fireRate;
    public float reloadTime;
    [HideInInspector] public bool reloading;
}

