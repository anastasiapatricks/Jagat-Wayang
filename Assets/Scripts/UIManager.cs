using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSpriteMap : SerializableDictionaryBase<string, Sprite> { }

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public ItemsPanel itemsPanel;
    public ItemSpriteMap itemSpriteMap;

    public void UpdatePanel(Item[] items) {
        itemsPanel.UpdateItems(items);
    }
}
