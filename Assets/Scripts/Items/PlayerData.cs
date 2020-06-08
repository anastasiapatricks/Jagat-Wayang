using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            new Attribute(100, 0),
            Resources.Load<Texture2D>("Parts/arjuna_body"));
        partMap[WayangPart.LeftUpperArm] = new PartItem(
            "Arjuna",
            PartType.UpperArm,
            new Attribute(10, 5),
            Resources.Load<Texture2D>("Parts/arjuna_upper_arm"));
        partMap[WayangPart.LeftLowerArm] = new PartItem(
            "Arjuna",
            PartType.LowerArm,
            new Attribute(10, 10),
            Resources.Load<Texture2D>("Parts/arjuna_lower_arm"));
        partMap[WayangPart.RightUpperArm] = new PartItem(
            "Arjuna",
            PartType.UpperArm,
            new Attribute(10, 5),
            Resources.Load<Texture2D>("Parts/arjuna_upper_arm"));
        partMap[WayangPart.RightLowerArm] = new PartItem(
            "Arjuna",
            PartType.LowerArm,
            new Attribute(10, 10),
            Resources.Load<Texture2D>("Parts/arjuna_lower_arm"));

        Item[] test = {
            new BlueprintItem(
                "Common",
                PartType.Body,
                new Attribute(50, 0),
                Resources.Load<Texture2D>("Blueprints/body_blueprint"),
                Resources.Load<Texture2D>("Blueprints/body_blueprint_mask")),
            new BlueprintItem(
                "Rare",
                PartType.Body,
                new Attribute(100, 0),
                Resources.Load<Texture2D>("Blueprints/body_blueprint1"),
                Resources.Load<Texture2D>("Blueprints/body_blueprint_mask")),
            new BlueprintItem(
                "Legendary Body",
                PartType.Body,
                new Attribute(200, 0),
                Resources.Load<Texture2D>("Blueprints/body_blueprint2"),
                Resources.Load<Texture2D>("Blueprints/body_blueprint_mask"))
        };
        foreach (var item in test)
        {
            inventory.Insert(item);
        }

        for (int i = 0; i < 3; i++)
        {
            inventory.Insert(new BlueprintItem(
                "Common",
                PartType.UpperArm,
                new Attribute(10, i * 5),
                Resources.Load<Texture2D>("Blueprints/upper_arm0"),
                Resources.Load<Texture2D>("Blueprints/upper_arm_mask")));
            inventory.Insert(new BlueprintItem(
                "Common",
                PartType.LowerArm,
                new Attribute(10, i * 5),
                Resources.Load<Texture2D>("Blueprints/lower_arm0"),
                Resources.Load<Texture2D>("Blueprints/lower_arm_mask")));
        }

        Texture2D[] textures = new Texture2D[] {
            Resources.Load<Texture2D>("Materials/wood1"),
            Resources.Load<Texture2D>("Materials/wood2"),
            Resources.Load<Texture2D>("Materials/leather1"),
            Resources.Load<Texture2D>("Materials/leather2"),
        };

        Vector2[][] shapes = new Vector2[][]
        {
            Shape.heh,
            Shape.hexagon,
            Shape.randd,
            Shape.square,
            Shape.triangle
        };

        System.Random rand = new System.Random();
        for (int i = 0; i < 40; i++)
        {
            int j = rand.Next(textures.Length);
            MaterialType materialType = (j == 0 || j == 1) ? MaterialType.Wood : MaterialType.Leather;
            Texture2D texture = textures[j];
            Vector2[] shape = shapes[rand.Next(shapes.Length)];
            int size = rand.Next(50, 100);
            int startX = rand.Next(texture.width - size);
            int startY = rand.Next(texture.height - size);
            inventory.Insert(MaterialItem.CreateFromShape(
                "Test Mat",
                materialType,
                new AddValueModifier(rand.Next(5, 30), rand.Next(3)),
                texture,
                shape,
                new Vector2Int(size, size),
                new Vector2Int(startX, startY)));
        }
    }
}