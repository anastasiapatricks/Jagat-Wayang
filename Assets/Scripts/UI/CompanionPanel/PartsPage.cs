using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PartsPage : MonoBehaviour
{
    public ToolTip attributeToolTip;
    public PartPicker partPicker;
    public Transform container;

    public Button[] wayangPartButtons;
    public Sprite current, unselected;

    private WayangPart selectedWayangPart;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wayangPartButtons.Length; i++)
        {
            int copy = i;
            wayangPartButtons[i].onClick.AddListener(
                () => SetWayangPartButton(copy));
        }
        SetWayangPartButton(0);

        partPicker.SetItemCallback(item => Swap((PartItem)item));

        UpdateContainer();
    }

    public void SetWayangPartButton(int val)
    {
        selectedWayangPart = (WayangPart)val;
        partPicker.SetPartType(selectedWayangPart.GetPartType());
        foreach (Button button in wayangPartButtons)
        {
            button.GetComponent<Image>().sprite = unselected;
        }
        wayangPartButtons[val].GetComponent<Image>().sprite = current;
    }

    private void Swap(PartItem part)
    {
        var partMap = Store.Instance.player.partMap;
        var inventory = Store.Instance.player.inventory;

        inventory.Insert(partMap[selectedWayangPart]);
        inventory.Remove(part);
        partMap[selectedWayangPart] = part;
        UpdateContainer();
    }

    void UpdateContainer()
    {
        for (int i = 0; i < 5; i++)
        {
            Texture2D texture = Store.Instance.player.partMap[(WayangPart)i].texture;
            container.GetChild(i).GetComponent<Image>().sprite = TextureHelper.GetSprite(texture);
        }

        attributeToolTip.Content = string.Format(
            "Total attribute\n<b>hp:</b> {0}\n<b>attack:</b> {1}",
            Store.Instance.player.GetTotalHp(),
            Store.Instance.player.GetTotalAttack());
    }
}
