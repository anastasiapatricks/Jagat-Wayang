using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CompanionPanel : UIComponent
{
    public Sprite current, unselected;

    public Image invTab, craftTab;
    public GameObject invPage, craftPage;

    private bool inFocus;

    public override void Focus()
    {
        inFocus = true;
        gameObject.SetActive(true);
    }

    public override void Blur()
    {
        inFocus = false;
    }

    void Update()
    {
        if (inFocus && Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            UIManager.Instance.RemoveFocus();
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
