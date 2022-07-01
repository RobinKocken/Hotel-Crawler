using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drugs Object", menuName = "Inventory Sytem/Items/Healing")]
public class HealingObject : ItemObject
{
    public void Awake()
    {
       type = ItemType.Healing;
    }
}
