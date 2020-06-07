using UnityEngine;
using System.Collections;
using System;

public class ItemPicker : MonoBehaviour
{
    public ItemGrid itemGrid;
    public ItemType itemType;

    private Inventory inventory;

    void Awake()
    {
        inventory = Store.Instance.player.inventory;

        inventory.onChangeListeners.Add(() =>
        {
            UpdateItemGrid();
        });
    }

    void Start()
    {
        UpdateItemGrid();
    }

    public void SetItemCallback(Action<Item> callback)
    {
        itemGrid.SetItemCallback(callback);
    }

    void UpdateItemGrid()
    {
        if (itemType == ItemType.Any)
        {
            itemGrid.SetItems(inventory.Items.ToArray());
        } else
        {
            itemGrid.SetItems(inventory.FilteredItem(itemType).ToArray());
        }
    }
}
