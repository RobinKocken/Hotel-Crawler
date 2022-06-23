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

    [Header("Particle")]
    public GameObject muzzlePos;
    public GameObject partHold;
    public float playParticle;



    float timeSinceLastShot;
    TextMeshProUGUI _curAmmoDisplay;
    TextMeshProUGUI _invAmmoDisplay;

    public void Awake()
    {
        gunData.reloading = false;
        _curAmmoDisplay = CurAmmoDisplay.GetComponent<TMPro.TextMeshProUGUI>();
        _invAmmoDisplay = InvAmmoDisplay.GetComponent<TMPro.TextMeshProUGUI>();
    }
    public void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        _curAmmoDisplay.text = gunData.currentAmmo.ToString();
        _invAmmoDisplay.text = gunData.AmmoInInventory.ToString();

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
        _curAmmoDisplay.text = gunData.currentAmmo.ToString();
        _invAmmoDisplay.text = gunData.AmmoInInventory.ToString();


        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        print("Shooting!!!");
        if(gunData.currentAmmo > 0)
        {
            if(CanShoot())
            {
                print("Shot!!!");
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    print("target hit: " + hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }
                gunData.currentAmmo--;
                _curAmmoDisplay.text = gunData.currentAmmo.ToString();
                timeSinceLastShot = 0;
                OnGunShot();
                print("removed bullet and shot particle");
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
    }
    private void OnGunShot()
    {
        GameObject newPar = Instantiate(muzzlePos, partHold.transform.position, Quaternion.identity);
        Destroy(newPar, playParticle);
    }
}
