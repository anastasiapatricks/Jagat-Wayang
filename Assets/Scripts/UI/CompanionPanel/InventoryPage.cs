using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public ItemGrid itemGrid;
    private Inventory inventory;
    private void Awake()
    {
        inventory = Store.Instance.player.inventory;
        inventory.onChangeListeners.Add(() => itemGrid.SetItems(inventory.Items.ToArray()));
    }

    private void Start()
    {
        itemGrid.SetItems(inventory.Items.ToArray());
    }
}
