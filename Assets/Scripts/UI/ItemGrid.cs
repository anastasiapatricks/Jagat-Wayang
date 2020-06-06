using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public int maxItems;

    public GameObject emptyCellPrefab;
    public GameObject itemIconPrefab;

    private Inventory inventory;
    private Action<Item> newItemHandler;
    private int inventoryStart;

    void Start()
    {
        //inventory = PlayerManager.Instance.inventory;

        //FillCells();
        //newItemHandler = (item) => UpdateCellContent();
        //inventory.AddNewItemListener(newItemHandler);
    }

    public void NextPage()
    {
        inventoryStart += maxItems;
        UpdateCellContent();
    }

    public void PreviousPage()
    {
        inventoryStart -= maxItems;
        if (inventoryStart < 0) inventoryStart = 0;
        UpdateCellContent();
    }

    private void OnDestroy()
    {
        inventory.RemoveListener(newItemHandler);
    }

    private void FillCells()
    {
        for (int i = 0; i < maxItems; i++)
        {
            GameObject cell = Instantiate(emptyCellPrefab, transform, false);
            cell.name = "item_" + i;
        }
    }

    private void UpdateCellContent()
    {
        Item[] items = inventory.Items.ToArray();
        for (int i = 0; i < maxItems; i++)
        {
            var invIndex = inventoryStart + i;
            SetItem(i, invIndex < items.Length ? items[invIndex] : null);
        }
    }

    private void SetItem(int i, Item item)
    {
        var cell = transform.GetChild(i);
        if (cell.childCount > 0)
        {
            if (cell.GetChild(0).GetComponent<ItemIcon>().item == item)
            {
                return;
            }
            Destroy(cell.GetChild(0).gameObject);
        }

        if (item != null)
        {
            GameObject newContent = ItemIcon.Create(itemIconPrefab, item);
            newContent.transform.SetParent(cell, false);
        }
    }
}
