using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }


    public Inventory inventory;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        inventory = new Inventory();
    }

    public void AddBarrel()
    {
        inventory.Insert(
            new Blueprint("Barrel", Resources.Load<Sprite>("Items/barrel"))
        );
    }

    public void AddKey()
    {
        inventory.Insert(
            new Blueprint("Key", Resources.Load<Sprite>("Items/key_silver"))
        );
    }
}
