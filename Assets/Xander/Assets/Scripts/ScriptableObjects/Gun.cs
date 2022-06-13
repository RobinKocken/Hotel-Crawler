using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;
    [Header("Ammo_UI")]
    public GameObject CurAmmoDisplay;
    public GameObject InvAmmoDisplay;


    float timeSinceLastShot;
    string _curAmmoDisplay;
    string _invAmmoDisplay;

    public void Awake()
    {
        gunData.reloading = false;
        _curAmmoDisplay = CurAmmoDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = gunData.currentAmmo.ToString();
        _invAmmoDisplay = CurAmmoDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = gunData.AmmoInInventory.ToString();
    }
    public void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if(!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        //removing bullets
        gunData.currentAmmo = gunData.magSize;
        gunData.AmmoInInventory -= gunData.magSize;
        //updating ammo on UI
        _curAmmoDisplay = gunData.currentAmmo.ToString();
        _invAmmoDisplay = gunData.AmmoInInventory.ToString();


        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
            if(CanShoot())
            {
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    print("target hit: " + hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }
                gunData.currentAmmo--;
                _curAmmoDisplay = gunData.currentAmmo.ToString();
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }
    private void OnGunShot()
    {
        
    }
}
