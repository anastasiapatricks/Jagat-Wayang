using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene : MonoBehaviour {
    public string characterName;
    [TextArea]
    public string dialog;

    public Image background;

    public Image leftSprite;
    public bool leftGreyOut;

    public Image rightSprite;
    public bool rightGreyOut;
}