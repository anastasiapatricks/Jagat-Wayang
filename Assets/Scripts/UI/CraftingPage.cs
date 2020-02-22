using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPage : MonoBehaviour
{
    MaterialItem wood, nyan;
    MaterialItem selectedMaterial;

    void SetSelectedMaterial(MaterialItem material)
    {
        selectedMaterial = material;
        GetComponentInChildren<PartEditor>().SetSelectedMaterial(material);
        GetComponentInChildren<PointerFollower>().SetImage(material.Sprite);
    }

    public void SetWood()
    {
        SetSelectedMaterial(wood);
    }

    public void SetNyan()
    {
        SetSelectedMaterial(nyan);
    }

    void Start()
    {
        Transform woodButton = transform.Find("Wood");
        wood = MaterialItem.CreateFromTemplate(
            "wood",
            new Vector2Int(50, 50),
            MaterialShape.randd,
            Resources.Load<Texture2D>("Materials/BaseTexture/wood"),
            Vector2Int.zero);
        woodButton.GetComponent<Image>().sprite = wood.Sprite;

        Transform nyanButton = transform.Find("Nyan");
        nyan = MaterialItem.CreateFromTemplate(
            "nyan",
            new Vector2Int(100, 100),
            MaterialShape.triangle,
            Resources.Load<Texture2D>("Materials/BaseTexture/nyan"),
            new Vector2Int(80, 40));
        nyanButton.GetComponent<Image>().sprite = nyan.Sprite;
    }

    void Update()
    {
        
    }
}
