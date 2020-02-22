using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartImage : MonoBehaviour
{
    private PartType type;
    private Texture2D texture;
    private bool[,] blueprintMask;

    public void SetPart(PartType type, Texture2D blueprint, Texture2D blueprintMask)
    {
        this.type = type;
        this.texture = MaterialTexture.Copy(blueprint);
        this.blueprintMask = MaterialShape.GenerateMask(blueprintMask);

        GetComponent<Image>().sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        GetComponent<RectTransform>().sizeDelta = new Vector2(texture.width, texture.height);
    }

    public void PasteMaterialOnCenter(Texture2D material, Vector2Int pos)
    {
        MaterialTexture.Paste(
            texture,
            blueprintMask,
            material,
            pos - new Vector2Int(material.width / 2, material.height / 2));
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPart(
            PartType.Body,
            Resources.Load<Texture2D>("Blueprints/body_blueprint"),
            Resources.Load<Texture2D>("Blueprints/body_blueprint_mask"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
