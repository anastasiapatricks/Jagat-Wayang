using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionPanel : MonoBehaviour
{
    public Sprite current, unselected;

    public Image invTab, craftTab;
    public GameObject invPage, craftPage;

    public GameObject companionPanel;

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            companionPanel.SetActive(false);
        }
    }

    public void SwitchInventory() {
        invTab.sprite = current;
        craftTab.sprite = unselected;

        invPage.SetActive(true);
        craftPage.SetActive(false);
    }

    public void SwitchCrafting() {
        invTab.sprite = unselected;
        craftTab.sprite = current;

        invPage.SetActive(false);
        craftPage.SetActive(true);
    }
}
