using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public Item item;
    public ToolTip toolTip;
    public Button button;

    private Action<Item> callback;

    public static GameObject Create(GameObject itemIconPrefab, Item item, Action<Item> callback = null)
    {
        GameObject itemIconClone = Instantiate(itemIconPrefab);
        ItemIcon itemIconComp = itemIconClone.GetComponent<ItemIcon>();
        itemIconComp.item = item;
        itemIconComp.callback = callback;
        return itemIconClone;
    }

    void Start()
    {
        GetComponent<Image>().sprite = item.sprite;
        toolTip.Content = item.GetDescription();
        if (callback != null)
        {
            button.onClick.AddListener(() => callback(item));
        }
    }
}
