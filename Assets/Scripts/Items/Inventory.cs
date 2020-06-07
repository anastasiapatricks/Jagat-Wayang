using System;
using System.Collections.Generic;

public class Inventory
{
    public List<Item> Items = new List<Item>();

    public List<Action<Item>> newItemListeners = new List<Action<Item>>();
    public List<Action> onChangeListeners = new List<Action>();

    public void Insert(Item item)
    {
        Items.Add(item);
        foreach (Action<Item> listener in newItemListeners)
        {
            listener(item);
        }
        foreach (Action listener in onChangeListeners)
        {
            listener();
        }
    }
    
    public void Remove(Item item)
    {
        Items.Remove(item);
        foreach (Action listener in onChangeListeners)
        {
            listener();
        }
    }

    public List<Item> FilteredItem(ItemType type)
    {
        return Items.FindAll((Item) => Item.type == type);
    }
}