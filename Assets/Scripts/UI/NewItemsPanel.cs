using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemsPanel : MonoBehaviour
{
    public GameObject itemIconPrefab;

    private CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        Store.Instance.player.inventory.newItemListeners.Add((item) =>
        {
            GameObject itemIcon = InsertNewItem(item);
            StartCoroutine(RemoveItemLater(itemIcon, 300));
        });
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    IEnumerator RemoveItemLater(GameObject itemIcon, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(itemIcon);
    }

    GameObject InsertNewItem(Item item)
    {
        Show();
        GameObject itemIcon = ItemIcon.Create(itemIconPrefab, item);
        itemIcon.transform.SetParent(transform, false);

        return itemIcon;
    }

    void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
