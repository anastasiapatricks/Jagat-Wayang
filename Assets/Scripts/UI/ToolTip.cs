using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public Text text;
    public float margin;
    private string _content;
    public string Content
    {
        get
        {
            return _content;
        }
        set
        {
            _content = value;
            text.text = value;
            Resize();
        }
    }

    private void Resize()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(
            text.preferredWidth + 2 * margin,
            text.preferredHeight + 2 * margin);
        text.GetComponent<RectTransform>().anchoredPosition = new Vector2(margin, -margin);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
