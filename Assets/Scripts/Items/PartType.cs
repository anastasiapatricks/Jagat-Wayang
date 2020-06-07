using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Body,
    UpperArm,
    LowerArm,
    Any
}

public enum WayangPart
{
    Body,
    LeftUpperArm,
    LeftLowerArm,
    RightUpperArm,
    RightLowerArm
}

static class PartTypeHelper
{
    public static PartType GetPartType(this WayangPart wayangPart)
    {
        switch (wayangPart)
        {
            case WayangPart.Body:
                return PartType.Body;

            case WayangPart.LeftUpperArm:
            case WayangPart.RightUpperArm:
                return PartType.UpperArm;

            case WayangPart.LeftLowerArm:
            case WayangPart.RightLowerArm:
                return PartType.LowerArm;

            default:
                return PartType.Any;
        }
    }
}