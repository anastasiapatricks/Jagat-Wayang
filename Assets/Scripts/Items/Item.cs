using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Blueprint,
    Material,
    Part,
    Any
}

public enum MaterialType
{
    Wood,
    Leather
}

public abstract class Item
{
    public string name;
    public ItemType type;
    public Sprite sprite;
    public Item(string name, ItemType type, Sprite sprite)
    {
        this.name = name;
        this.type = type;
        this.sprite = sprite;
    }

    public abstract string GetDescription();
}

public class BlueprintItem : Item
{
    public PartType partType;
    public Attribute baseAttribute;
    public Texture2D blueprint;
    public Texture2D mask;

    public BlueprintItem(string name, PartType partType, Attribute baseAttribute,
                         Texture2D blueprint, Texture2D mask) :
        base(name, ItemType.Blueprint, CreateSprite(blueprint))
    {
        this.partType = partType;
        this.baseAttribute = baseAttribute;
        this.blueprint = blueprint;
        this.mask = mask;
    }

    public override string GetDescription()
    {
        return string.Format(
            "{0} | {1} | {2}\n{3}",
            name,
            type.ToString(),
            partType.ToString(),
            baseAttribute.GetDescription());
    }

    private static Sprite CreateSprite(Texture2D blueprint)
    {
        return Sprite.Create(
            blueprint,
            new Rect(0, (blueprint.height - blueprint.width)/2, blueprint.width, blueprint.width),
            new Vector2(0.5f, 0.5f));
    }
}

public class MaterialItem : Item
{
    public MaterialType materialType;
    public IModifier modifier;
    public Texture2D texture;

    public MaterialItem(string name, MaterialType materialType, IModifier modifier, Texture2D texture) :
        base(name, ItemType.Material, CreateSprite(texture))
    {
        this.materialType = materialType;
        this.modifier = modifier;
        this.texture = texture;
    }

    public static MaterialItem CreateFromShape(
        string name, MaterialType materialType, IModifier modifier,
        Texture2D baseTexture, Vector2[] shape, Vector2Int size, Vector2Int baseStart)
    {
        bool[,] shapeMask = Shape.PolygonFillMask(size.y, size.x, shape, true);
        Texture2D texture = TextureHelper.CreateTextureFromMask(shapeMask, baseTexture, baseStart);

        return new MaterialItem(name, materialType, modifier, texture);
    }

    public override string GetDescription()
    {
        return string.Format("{0} | {1} | {2}\n{3}",
            name,
            type.ToString(),
            materialType.ToString(),
            modifier.GetDescription());
    }

    private static Sprite CreateSprite(Texture2D texture)
    {
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
    }
}

public class PartItem : Item
{
    public Attribute attribute;
    public PartType partType;
    public Texture2D texture;

    public PartItem(string name, PartType partType, Attribute attribute, Texture2D texture) :
    base(name, ItemType.Part, CreateSprite(texture))
    {
        this.attribute = attribute;
        this.partType = partType;
        this.texture = texture;
    }

    public override string GetDescription()
    {
        return string.Format(
            "{0} | {1} | {2}\n{3}",
            name,
            type.ToString(),
            partType.ToString(),
            attribute.GetDescription());
    }

    private static Sprite CreateSprite(Texture2D texture)
    {
        return Sprite.Create(
            texture,
            new Rect(0, (texture.height - texture.width)/2, texture.width, texture.width),
            new Vector2(0.5f, 0.5f));
    }
}
