using System;
using System.Collections.Generic;

public class Inventory
{
    public List<Item> Items = new List<Item>();

    private List<Action<Item>> newItemListeners = new List<Action<Item>>();

    public void AddNewItemListener(Action<Item> listener)
    {
        newItemListeners.Add(listener);
    }

    public void RemoveListener(Action<Item> listener)
    {
        newItemListeners.Remove(listener);
    }

    public void Insert(Item item)
    {
        Items.Add(item);
        foreach (Action<Item> listener in newItemListeners)
        {
            listener(item);
        }
    }
    
    public bool Remove(Item item)
    {
        return Items.Remove(item);
    }

    public List<Item> FilteredItem(ItemType type)
    {
        return Items.FindAll((Item) => Item.Type == type);
    }
}