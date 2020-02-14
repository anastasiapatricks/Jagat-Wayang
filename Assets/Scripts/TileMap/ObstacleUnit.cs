using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUnit : GameUnit
{
    // Start is called before the first frame update
    void Start()
    {
        unitType = UnitType.Obstacle;
        TileMap.Instance.SetUnit(this, GetPos());
    }

    public void DestroySelf() {
        TileMap.Instance.SetUnit(null, GetPos());
        Destroy(gameObject);
    }
}
