using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerFollower : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
        image.color = Color.white;
        rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
    }

    public void ResetImage()
    {
        image.sprite = null;
        image.color = Color.clear;
    }

    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
