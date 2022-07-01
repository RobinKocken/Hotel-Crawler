using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory Sytem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for(int i = 0; i <Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    public void RemoveItem(ItemObject item, int _amount)
    {
        
        foreach (var itemSlot in Container)
        {
            if (itemSlot.item == item && itemSlot.amount > 0)
            {
                itemSlot.amount -= _amount;
                if(itemSlot.amount < 0)
                {
                    itemSlot.amount = 0;
                }
            }  
        }
    }
    public int GetAmount(ItemObject item)
    {
        int amount = 0;
        foreach (var itemSlot in Container)
        {
            if (itemSlot.item == item)
            {
                amount += itemSlot.amount;
            }
                
        }
        return amount;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}