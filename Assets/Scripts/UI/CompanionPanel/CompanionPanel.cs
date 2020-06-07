using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CompanionPanel : UIComponent
{
    public Sprite current, unselected;

    public Button[] tabs;
    public GameObject[] pages;

    private bool inFocus;

    private void Awake()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            int copy = i;
            tabs[i].onClick.AddListener(() => Switch(copy));
        }
        Switch(0);
    }

    private void Switch(int i)
    {
        foreach (Button tab in tabs)
        {
            tab.GetComponent<Image>().sprite = unselected;
        }
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        tabs[i].GetComponent<Image>().sprite = current;
        pages[i].SetActive(true);
    }

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
}
