using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPage : MonoBehaviour
{
    public ItemPicker blueprintPicker;
    public ItemPicker materialPicker;
    public PartEditor partEditor;
    public ToolTip attributeTooltip;

    private Inventory inventory;
    private BlueprintItem blueprint;
    private MaterialItem previewMaterial;
    private List<MaterialItem> pastedMaterials = new List<MaterialItem>();

    private void Awake()
    {
        inventory = Store.Instance.player.inventory;
        blueprintPicker.SetItemCallback((item) => SetBlueprint((BlueprintItem)item));
        materialPicker.SetItemCallback((item) => SetPreviewMaterial(previewMaterial != item ? (MaterialItem)item : null));
        partEditor.SetOnPasteCallback((materialItem) =>
        {
            if (materialItem == null) return;

            SetPreviewMaterial(null);
            pastedMaterials.Add(materialItem);
            UpdateAttributeTooltip();
            inventory.Remove(materialItem);
        });
        ShowBlueprintPicker();
    }

    public void SetPreviewMaterial(MaterialItem material)
    {
        previewMaterial = material;
        partEditor.SetPreviewMaterial(material);
    }

    public void SetBlueprint(BlueprintItem blueprint)
    {
        this.blueprint = blueprint;
        partEditor.SetBlueprint(blueprint);
    }

    public void SelectBlueprint()
    {
        if (blueprint == null) return;
        ShowMaterialPicker();
    }

    public void CancelPickMaterial()
    {
        foreach (Item material in pastedMaterials)
        {
            inventory.Insert(material);
        }
        pastedMaterials.Clear();
        partEditor.SetBlueprint(blueprint);
        SetPreviewMaterial(null);
        ShowBlueprintPicker();
    }

    public void CommitPickMaterial()
    {
        inventory.Remove(blueprint);
        inventory.Insert(new PartItem(
            blueprint.name,
            blueprint.partType,
            GetFinalAttribute(),
            partEditor.GetPartTexture()
        ));

        pastedMaterials.Clear();
        SetBlueprint(null);
        SetPreviewMaterial(null);
        ShowBlueprintPicker();
    }

    private Attribute GetFinalAttribute()
    {
        Attribute attribute = blueprint.baseAttribute.Copy();
        foreach (MaterialItem material in pastedMaterials)
        {
            attribute = material.modifier.UpdateAttribute(attribute);
        }
        return attribute;
    }

    private void UpdateAttributeTooltip()
    {
        Attribute finalAttribute = GetFinalAttribute();
        Attribute baseAttribute = blueprint.baseAttribute;
        attributeTooltip.Content = string.Format(
            "{0} | {1}\n<b>hp:</b> {2}\n<b>attack:</b> {3}",
            blueprint.name, blueprint.partType.ToString(),
            string.Format("{0}+{1}", baseAttribute.hp, finalAttribute.hp - baseAttribute.hp),
            string.Format("{0}+{1}", baseAttribute.attack, finalAttribute.attack - baseAttribute.attack));
    }

    private void ShowBlueprintPicker()
    {
        blueprintPicker.gameObject.SetActive(true);
        materialPicker.gameObject.SetActive(false);
        attributeTooltip.Hide();
    }

    private void ShowMaterialPicker()
    {
        blueprintPicker.gameObject.SetActive(false);
        materialPicker.gameObject.SetActive(true);
        UpdateAttributeTooltip();
        attributeTooltip.Show();
    }
}
