using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartEditor : MonoBehaviour, IPointerClickHandler
{
    private PartImage partImage;
    private PointerFollower pointerFollower;

    private MaterialItem previewMaterial;
    private Action<MaterialItem> onPasteCallback;

    void Start()
    {
        partImage = GetComponentInChildren<PartImage>();
        pointerFollower = GetComponentInChildren<PointerFollower>();
    }

    public void SetBlueprint(BlueprintItem blueprint)
    {
        if (blueprint == null)
        {
            partImage.Clear();
        } else
        {
            partImage.SetFromBlueprint(blueprint);
        }
    }

    public void SetPreviewMaterial(MaterialItem material)
    {
        previewMaterial = material;
        if (material != null)
        {
            pointerFollower.SetImage(material.sprite);
        } else
        {
            pointerFollower.ResetImage();
        }
    }

    public void SetOnPasteCallback(Action<MaterialItem> callback)
    {
        onPasteCallback = callback;
    }

    public Texture2D GetPartTexture()
    {
        return partImage.texture;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (previewMaterial == null) return;

        Vector2Int GetPartPos()
        {
            RectTransform rect = partImage.GetComponent<RectTransform>();
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect, pointerEventData.position, null, out pointerPos);
            pointerPos += rect.sizeDelta / 2;
            return new Vector2Int((int) pointerPos.x, (int) pointerPos.y);
        }

        partImage.PasteMaterial(
            previewMaterial.texture,
            GetPartPos());
        onPasteCallback(previewMaterial);
    }
}
