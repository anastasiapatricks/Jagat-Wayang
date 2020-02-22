using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : Item
{
    public MaterialItem(string name, Sprite sprite) : base(name, ItemType.Material, sprite)
    { }

    public static MaterialItem CreateFromTemplate(
        string name,
        Vector2Int size,
        Vector2[] shape,
        Texture2D baseTexture,
        Vector2Int baseStart)
    {
        bool[,] shapeMask = MaterialShape.PolygonFillMask(size.y, size.x, shape, true);
        Texture2D texture = MaterialTexture.CreateTextureFromMask(shapeMask, baseTexture, baseStart);
        return new MaterialItem(name, Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)));
    }
}
