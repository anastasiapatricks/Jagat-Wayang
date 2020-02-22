using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestToolTip : ToolTip
{
    public override void UpdateContent()
    {
        base.UpdateContent();
        GetComponentInChildren<Text>().text = Item.Name;
    }
}
