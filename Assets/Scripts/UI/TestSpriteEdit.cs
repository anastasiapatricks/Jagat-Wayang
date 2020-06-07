using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSpriteEdit : MonoBehaviour
{
    Texture2D origBlueprint, blueprint;
    Texture2D wood, nyan;
    bool[,] mask;
    bool once = false;

    int x, y;

    void Start()
    {
        origBlueprint = TextureHelper.Copy(Resources.Load<Texture2D>("Blueprints/body_blueprint"));
        mask = Shape.GenerateMask(
            Resources.Load<Texture2D>("Blueprints/body_blueprint_mask")
        );
        wood = Resources.Load<Texture2D>("Materials/BaseTexture/wood");
        nyan = Resources.Load<Texture2D>("Materials/BaseTexture/nyan");
        UpdateSprite();
    }

    public void Heh()
    {
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        bool[,] shapeMask = Shape.PolygonFillMask(512, 256, Shape.square, true);
        Texture2D shape = TextureHelper.CreateTextureFromMask(
            shapeMask,
            nyan,
            new Vector2Int(x, y));

        blueprint = TextureHelper.Copy(origBlueprint);
        TextureHelper.Paste(blueprint, mask, shape, Vector2Int.zero);

        GetComponent<Image>().sprite = TextureHelper.GetSprite(blueprint);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) x -= 20;
        if (Input.GetKey(KeyCode.RightArrow)) x += 20;
        if (Input.GetKey(KeyCode.DownArrow)) y -= 20;
        if (Input.GetKey(KeyCode.UpArrow)) y += 20;
        if (Input.anyKey) UpdateSprite();
    }
}
