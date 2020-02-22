using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartEditor : MonoBehaviour, IPointerClickHandler
{
    private MaterialItem selectedMaterial;

    public void SetSelectedMaterial(MaterialItem selectedMaterial)
    {
        this.selectedMaterial = selectedMaterial;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Transform part = transform.GetChild(0);

        Vector2Int GetPartPos()
        {
            RectTransform rect = part.GetComponent<RectTransform>();
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect, pointerEventData.position, null, out pointerPos);
            pointerPos += rect.sizeDelta / 2;
            return new Vector2Int((int) pointerPos.x, (int) pointerPos.y);
        }

        if (selectedMaterial != null)
        {
            part.GetComponent<PartImage>().PasteMaterialOnCenter(
                selectedMaterial.Sprite.texture,
                GetPartPos());
        }
    }
}
