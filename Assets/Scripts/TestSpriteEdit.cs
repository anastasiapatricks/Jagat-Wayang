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
        origBlueprint = MaterialTexture.Copy(Resources.Load<Texture2D>("Blueprints/body_blueprint"));
        mask = MaterialShape.GenerateMask(
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
        bool[,] shapeMask = MaterialShape.PolygonFillMask(512, 256, MaterialShape.square, true);
        Texture2D shape = MaterialTexture.CreateTextureFromMask(
            shapeMask,
            nyan,
            new Vector2Int(x, y));

        blueprint = MaterialTexture.Copy(origBlueprint);
        MaterialTexture.Paste(blueprint, mask, shape, Vector2Int.zero);

        GetComponent<Image>().sprite = Sprite.Create(
            blueprint,
            new Rect(0, 0, blueprint.width, blueprint.height),
            new Vector2(0.5f, 0.5f));
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
