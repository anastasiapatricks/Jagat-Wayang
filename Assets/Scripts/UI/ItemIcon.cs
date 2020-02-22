using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public GameObject toolTipPrefab;
    public Vector3 toolTipOffset;

    public Item item;

    private GameObject toolTip;
    private CanvasGroup toolTipCanvas;

    public static GameObject Create(GameObject itemIconPrefab, Item item)
    {
        GameObject itemIconClone = GameObject.Instantiate(itemIconPrefab);
        ItemIcon itemIconComp = itemIconClone.GetComponent<ItemIcon>();
        itemIconComp.item = item;
        return itemIconClone;
    }

    void Start()
    {
        GetComponent<Image>().sprite = item.Sprite;
        if (toolTipPrefab != null)
        {
            toolTip = Instantiate(toolTipPrefab, transform, false);
            toolTip.transform.localPosition = toolTipOffset;
            //print(item);

            toolTip.GetComponent<ToolTip>().Item = item;
            toolTipCanvas = toolTip.GetComponent<CanvasGroup>();
        }
        HideTextInfo();
    }

    public void ShowTextInfo()
    {
        toolTipCanvas.alpha = 1f;
        toolTipCanvas.blocksRaycasts = true;
    }
    public void HideTextInfo()
    {
        toolTipCanvas.alpha = 0f;
        toolTipCanvas.blocksRaycasts = false;
    }
}
