using UnityEngine;

public class Store : MonoBehaviour
{
    private static Store _instance;
    public PlayerData player;

    public static Store Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Instance.player.inventory.ResetListeners();
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        player = new PlayerData();
    }
}
