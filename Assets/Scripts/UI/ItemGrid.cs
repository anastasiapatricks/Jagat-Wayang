using System;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public int maxItems;

    public GameObject emptyCellPrefab;
    public GameObject itemIconPrefab;

    private Item[] items;
    private int startIndex;

    private Action<Item> itemCallback;

    void Awake()
    {
        // Fill empty cell
        for (int i = 0; i < maxItems; i++)
        {
            GameObject cell = Instantiate(emptyCellPrefab, transform, false);
            cell.name = "item_" + i;
        }
    }

    public void SetItemCallback(Action<Item> callback)
    {
        itemCallback = callback;
    }

    public void SetItems(Item[] items)
    {
        this.items = items;
        UpdateCellContent();
    }

    public void NextPage()
    {
        startIndex += maxItems;
        UpdateCellContent();
    }

    public void PreviousPage()
    {
        startIndex -= maxItems;
        if (startIndex < 0) startIndex = 0;
        UpdateCellContent();
    }

    public void UpdateCellContent()
    {
        for (int i = 0; i < maxItems; i++)
        {
            var invIndex = startIndex + i;
            SetItem(i, invIndex < items.Length ? items[invIndex] : null);
        }
    }

    private void SetItem(int i, Item item)
    {
        var cell = transform.GetChild(i);
        foreach (Transform child in cell)
        {
            Destroy(child.gameObject);
        }
        if (item != null)
        {
            GameObject newContent = ItemIcon.Create(itemIconPrefab, item, itemCallback);
            newContent.transform.SetParent(cell, false);
        }
    }
}
