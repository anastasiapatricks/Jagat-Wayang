using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubLevelScript : LevelScript
{
    public override void OnSceneLoad()
    {
        var inventory = Store.Instance.player.inventory;
        inventory.ResetListeners();
    }
}
