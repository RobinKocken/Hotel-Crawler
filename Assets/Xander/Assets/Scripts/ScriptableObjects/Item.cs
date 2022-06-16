using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ammo")]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemDescription;

    [Header("Effects")]
    public int ammount;

}
