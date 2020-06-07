using UnityEngine;
using System.Collections;
using System;

public class PartPicker : MonoBehaviour
{
    public ItemGrid itemGrid;
    public PartType partType;

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

    public void SetPartType(PartType partType)
    {
        this.partType = partType;
        UpdateItemGrid();
    }

    public void SetItemCallback(Action<Item> callback)
    {
        itemGrid.SetItemCallback(callback);
    }

    void UpdateItemGrid()
    {
        itemGrid.SetItems(inventory.FilteredItem(ItemType.Part).FindAll(item =>
            ((PartItem)item).partType == partType).ToArray());
    }
}
