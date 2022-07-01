using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Object", menuName = "Inventory Sytem/Items/Ammo")]
public class AmmoObject : ItemObject
{
    
    public void Awake()
    {
        type = ItemType.Ammo;
}
}
