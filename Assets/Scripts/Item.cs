using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Blueprint,
    Material
}

public class Item
{
    public Item(string name, ItemType type, Sprite sprite)
    {
        Name = name;
        Type = type;
        Sprite = sprite;
    }

    public string Name { get; }

    public ItemType Type { get; }

    public Sprite Sprite { get; }

    public string GetDesc()
    {
        return Name + Type.ToString();
    }

    public static Item[] CreateSampleItems()
    {
        return new[] {
            new Item("Barrel", ItemType.Blueprint, Resources.Load<Sprite>("Items/barrel")),
            new Item("Key", ItemType.Material, Resources.Load<Sprite>("Items/key_silver")),
            new Item("Table", ItemType.Blueprint, Resources.Load<Sprite>("Items/table")),
            new Item("Flag", ItemType.Material, Resources.Load<Sprite>("Items/flag_red"))
        };
    }
}
