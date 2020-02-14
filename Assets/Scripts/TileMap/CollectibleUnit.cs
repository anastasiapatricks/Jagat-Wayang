using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleUnit : GameUnit { 
    // Start is called before the first frame update
    void Start() {
        unitType = UnitType.Collectible;
        TileMap.Instance.SetUnit(this, GetPos());
    }
}
