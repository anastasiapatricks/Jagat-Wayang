using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MovableGameUnit
{
    public float pushedTime = 0.5f;
    public string characterName;

    // Start is called before the first frame update
    void Start()
    {
        unitType = UnitType.Character;
        TileMap.Instance.SetUnit(this, GetPos());
    }

    private Vector3 ToWorldPos(Vector2Int pos) {
        return new Vector3(pos.x, transform.position.y, pos.y);
    }

    public override void Move(Vector2Int nextPos) {
        LeanTween.move(gameObject, ToWorldPos(nextPos), pushedTime).setOnComplete(() => {
            // check if tile has Trigger component
            // if yes, fire action to destroy wall
            Trigger trigger = TileMap.Instance.GetTile(nextPos).GetComponent<Trigger>();
            if (trigger != null) {
                trigger.Fire();
            }
        });
    }
}
