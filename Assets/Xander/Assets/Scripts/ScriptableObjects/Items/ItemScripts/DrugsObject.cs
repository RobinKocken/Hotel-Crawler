using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drugs Object", menuName = "Inventory Sytem/Items/Drugs")]

public class DrugsObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Drugs;
    }
}
    
