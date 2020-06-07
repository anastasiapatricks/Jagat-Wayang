using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartMapper : MonoBehaviour
{
    public SpriteRenderer body, leftUpperArm, leftLowerArm, rightUpperArm, rightLowerArm;

    void Awake()
    {
        var partMap = Store.Instance.player.partMap;
        body.sprite = TextureHelper.GetSprite(
            partMap[WayangPart.Body].texture,
            body.sprite);
        leftUpperArm.sprite = TextureHelper.GetSprite(
            partMap[WayangPart.LeftUpperArm].texture,
            leftUpperArm.sprite);
        leftLowerArm.sprite = TextureHelper.GetSprite(
            partMap[WayangPart.LeftLowerArm].texture,
            leftLowerArm.sprite);
        rightUpperArm.sprite = TextureHelper.GetSprite(
            partMap[WayangPart.RightUpperArm].texture,
            rightUpperArm.sprite);
        rightLowerArm.sprite = TextureHelper.GetSprite(
            partMap[WayangPart.RightLowerArm].texture,
            rightLowerArm.sprite);

        GetComponent<Player>().playerMaxHealth = (int)Store.Instance.player.GetTotalHp();
        GetComponent<PlayerCombat>().attackDamage = (int)Store.Instance.player.GetTotalAttack();
    }
}
