using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class PartImage : MonoBehaviour
{
    public PartType type;
    public Texture2D blueprintSource;
    public Texture2D maskSource;

    public Texture2D texture;
    private bool[,] blueprintMask;

    public void SetFromBlueprint(BlueprintItem blueprintItem)
    {
        type = blueprintItem.partType;
        blueprintSource = blueprintItem.blueprint;
        maskSource = blueprintItem.mask;
        Make();
    }

    public void Clear()
    {
        type = PartType.Any;
        blueprintSource = null;
        maskSource = null;
        GetComponent<Image>().sprite = null;
        GetComponent<RectTransform>().sizeDelta = Vector2.zero;
    }

    private void Make()
    {
        texture = TextureHelper.Copy(blueprintSource);
        blueprintMask = Shape.GenerateMask(maskSource);

        GetComponent<Image>().sprite = TextureHelper.GetSprite(texture);
        GetComponent<RectTransform>().sizeDelta = new Vector2(texture.width, texture.height);
    }

    public void PasteMaterial(Texture2D materialTexture, Vector2Int pos)
    {
        TextureHelper.Paste(
            texture,
            blueprintMask,
            materialTexture,
            pos - new Vector2Int(materialTexture.width / 2, materialTexture.height / 2));
    }
}
