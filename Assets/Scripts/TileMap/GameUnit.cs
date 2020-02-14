using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Identify different types of game units
public enum UnitType {
    None,
    Player,
    Character,  // Arjuna, Dursasana
    Gunungan,   // Palace, Forest, Barren  
    Collectible,
    Obstacle
}

public abstract class GameUnit : MonoBehaviour
{
    public UnitType unitType;

    public Vector2Int GetPos() {
        var pos = transform.position;
        return new Vector2Int((int) pos.x, (int) pos.z);
    }
}

public abstract class MovableGameUnit : GameUnit {
    public abstract void Move(Vector2Int newPos);
}

