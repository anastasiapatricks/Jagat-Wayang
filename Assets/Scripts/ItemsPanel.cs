using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanel : MonoBehaviour
{
    public GameObject itemIcon;

    private CanvasGroup canvasGroup;
    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void UpdateItems(Item[] items) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (var item in items) {
            GameObject clone = Instantiate(itemIcon, transform);
            //clone.GetComponent<Image>().sprite = item.sprite;
        }

        if (transform.childCount > 0) {
            Show();
        } else {
            Hide();
        }
    }

    void Show() {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    void Hide() {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}
