using System.Collections.Generic;
using UnityEngine;

public class Blueprint : Item
{
    private List<IModifier> modifiers;

    public Blueprint(string name, Sprite sprite, List<IModifier> modifiers = null) : base(name, ItemType.Blueprint, sprite)
    {
        this.modifiers = modifiers ?? new List<IModifier>();
    }

    public string GetModifierDesc()
    {
        return "Modifier Desc";
    }
}