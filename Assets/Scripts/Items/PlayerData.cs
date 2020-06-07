using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Transform lastHubPosition;
    public Inventory inventory = new Inventory();
    public Dictionary<WayangPart, PartItem> partMap = new Dictionary<WayangPart, PartItem>();

    public float GetTotalHp()
    {
        float totalHp = 0;
        foreach (PartItem part in partMap.Values)
        {
            totalHp += part.attribute.hp;
        }
        return totalHp;
    }

    public float GetTotalAttack()
    {
        float totalAttack = 0;
        foreach (PartItem part in partMap.Values)
        {
            totalAttack += part.attribute.attack;
        }
        return totalAttack;
    }
    public PlayerData()
    {
        partMap[WayangPart.Body] = new PartItem(
            "Arjuna",
            PartType.Body,
            new Attribute(100, 100),
            Resources.Load<Texture2D>("Parts/arjuna_body"));
        partMap[WayangPart.LeftUpperArm] = new PartItem(
            "Arjuna",
            PartType.UpperArm,
            new Attribute(100, 100),
            Resources.Load<Texture2D>("Parts/arjuna_upper_arm"));
        partMap[WayangPart.LeftLowerArm] = new PartItem(
            "Arjuna",
            PartType.LowerArm,
            new Attribute(100, 100),
            Resources.Load<Texture2D>("Parts/arjuna_lower_arm"));
        partMap[WayangPart.RightUpperArm] = new PartItem(
            "Arjuna",
            PartType.UpperArm,
            new Attribute(100, 100),
            Resources.Load<Texture2D>("Parts/arjuna_upper_arm"));
        partMap[WayangPart.RightLowerArm] = new PartItem(
            "Arjuna",
            PartType.LowerArm,
            new Attribute(100, 100),
            Resources.Load<Texture2D>("Parts/arjuna_lower_arm"));

        Item[] test = {
            new BlueprintItem(
                "heh",
                PartType.Body,
                new Attribute(50, 0),
                Resources.Load<Texture2D>("Blueprints/body_blueprint"),
                Resources.Load<Texture2D>("Blueprints/body_blueprint_mask")),
            new BlueprintItem(
                "heh",
                PartType.Body,
                new Attribute(100, 0),
                Resources.Load<Texture2D>("Blueprints/body_blueprint1"),
                Resources.Load<Texture2D>("Blueprints/body_blueprint_mask")),
            new BlueprintItem(
                "heh",
                PartType.Body,
                new Attribute(150, 0),
                Resources.Load<Texture2D>("Blueprints/body_blueprint2"),
                Resources.Load<Texture2D>("Blueprints/body_blueprint_mask")),
            new BlueprintItem(
                "heh",
                PartType.LowerArm,
                new Attribute(5, 50),
                Resources.Load<Texture2D>("Blueprints/lower_arm0"),
                Resources.Load<Texture2D>("Blueprints/lower_arm_mask")),
            MaterialItem.CreateFromShape(
                "mat1",
                MaterialType.Wood,
                new AddValueModifier(30, 10),
                Resources.Load<Texture2D>("Materials/wood"),
                Shape.triangle,
                new Vector2Int(100, 100),
                new Vector2Int(0, 0))
        };
        foreach (var item in test)
        {
            inventory.Insert(item);
        }
    }
}