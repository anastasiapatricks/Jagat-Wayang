using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileHighlightMap : SerializableDictionaryBase<TileHighlight, Material> { }

[System.Serializable]
public class TileMaterialMap : SerializableDictionaryBase<string, Material> { }

public class TileMap : MonoBehaviour {
    public GameObject baseTile;
    public TileMaterialMap tileMaterialMap;
    public TileHighlightMap tileHighlightMap;   // safe, potentialUnsafe, currentUnsafe, nextUnsafe

    // Array to hold information of game units: Player, CompanionCube, Key
    // public GameUnit[] gameUnits;

    const int mapSizeX = 11;
    const int mapSizeY = 7;

    Tile[,] tiles = new Tile[mapSizeX, mapSizeY];

    private static TileMap _instance;

    public static TileMap Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        GenerateTileData();
        //GenerateMapData();      // Array allocation for the map tiles
        //GenerateMapVisual();    // Spawning visual prefab
    }

    private void Start() {
        IEnumerator FlashRandom() {
            for (var x = 0; x < mapSizeX; x++) {
                for (var y = 0; y < mapSizeY; y++) {
                    Tile tile = tiles[x, y];
                    //if (!tile.isSafe) {
                    //    var val = Random.value;
                    //    tile.SetUnsafeState(val < 0.4 ? UnsafeState.None : val < 0.7 ? UnsafeState.Current : UnsafeState.Next);
                    //}
                }
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(FlashRandom());
        }

        NextState();

        // StartCoroutine(FlashRandom());
    }

    void GenerateTileData() {
        foreach (Transform tileObject in transform) {
            Tile tile = tileObject.GetComponent<Tile>();
            Vector2Int pos = tile.GetPos();
            tiles[pos.x, pos.y] = tile;
        }
    }

    //void GenerateMapData() {
    //    // Allocate array size for map tiles
    //    tiles = new Tile[mapSizeX, mapSizeY];

    //    // Initialise map tiles to default tiles
    //    for (int x = 0; x < mapSizeX; x++) {
    //        for (int y = 0; y < mapSizeY; y++) {
    //            //var a = (x + y) % 3;
    //            //tiles[x, y] = new Tile(a == 0 ? "cat" : a == 1 ? "asteroid" : "leaves", Random.value < 0.5);
    //        }
    //    }

    //    //// Choose potential unsafe tile
    //    //tiles[2, 0] = new Tile("cat", TileType.PotentialUnsafe);
    //    //tiles[2, 1] = new Tile("cat", TileType.PotentialUnsafe);
    //    //tiles[0, 2] = new Tile("cat", TileType.PotentialUnsafe);
    //    //tiles[1, 2] = new Tile("cat", TileType.PotentialUnsafe);
    //    //tiles[2, 2] = new Tile("cat", TileType.PotentialUnsafe);

    //    // TODO
    //    // randomise currentUnsafe and nextUnsafe

    //}

    // Spawning visual prefab
    //void GenerateMapVisual() {
    //    for (int x = 0; x < mapSizeX; x++) {
    //        for (int y = 0; y < mapSizeY; y++) {
    //            //GameObject go = tiles[x, y].GenerateVisual(new Vector2Int(x, y));
    //            //go.transform.parent = transform;
    //        }
    //    }
    //}

    //public void Generate() {
    //    for (int x = 0; x < mapSizeX; x++) {
    //        for (int y = 0; y < mapSizeY; y++) {
    //            var map = TileMap.Instance;
    //            GameObject go = GameObject.Instantiate(
    //                map.baseTile,
    //                new Vector3(x, 0, y),
    //                Quaternion.identity);

    //            go.transform.parent = transform;

    //            go.name = "Tile (" + x + ", " + y + ")";

    //            go.GetComponent<Tile>().SetAttributes("floor", true);

    //        }
    //    }
    //}

    public Vector2Int SearchUnitPos(GameUnit unit) {    // Search for unit coordinate
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                if (unit == tiles[x, y].unit) {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public void SetUnit(GameUnit unit, Vector2Int pos) {
        tiles[pos.x, pos.y].unit = unit;
    }

    public void MoveUnit(MovableGameUnit unit, Vector2Int newPos) {   // Update state in `tiles` (Tile[])
        var originPos = SearchUnitPos(unit);
        // update new position
        SetUnit(unit, newPos);
        SetUnit(null, originPos);
        unit.Move(newPos);
    }

    public bool IsValidPos(Vector2Int pos) {
        return pos.x >= 0 && pos.x < mapSizeX && pos.y >= 0 && pos.y < mapSizeY;
    }

    public GameUnit GetUnit(Vector2Int pos) {
        return tiles[pos.x, pos.y].unit;
    }

    public Tile GetTile(Vector2Int pos) {
        return tiles[pos.x, pos.y];
    }

    public Tile[] GetUnsafeTiles(UnsafeState? unsafeState = null) {
        List<Tile> unsafeTiles = new List<Tile>();

        for (var x = 0; x < mapSizeX; x++) {
            for (var y = 0; y < mapSizeY; y++) {
                Tile tile = tiles[x, y];
                
                // unsafeTiles are manually selected in Unity
                if (!tile.isSafe) {
                    if (unsafeState == null || tile.unsafeState == unsafeState) {
                        unsafeTiles.Add(tile);
                    }
                }
            }
        }

        return unsafeTiles.ToArray();
    }
    
    //Rules:
    //- orangeTile -> redTile
    //- redTile !-> orangeTile
    //- 0.2 * #pinkTiles < #orangeTile <= 0.3 * #pinkTiles
    public void NextState() {
        void Shuffle<T>(T[] list) {
            int n = list.Length;
            while (n > 1) {
                n--;
                int k = Random.Range(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        Tile[] allUnsafeTiles = GetUnsafeTiles();

        // check unsafeTile[] content, change according to rules
        foreach (var tile in allUnsafeTiles) {
            if (tile.unsafeState == UnsafeState.Next) {
                tile.SetUnsafeState(UnsafeState.Current);
            }
            else if (tile.unsafeState == UnsafeState.Current) {
                tile.SetUnsafeState(UnsafeState.None);
            }
        }


        // randomise UnsafeState
        // shuffle unsafeTilesArray
        // set the first 0.3 entries as UnsafeState.Next

        int nextUnsafeCount = (int) (0.3 * allUnsafeTiles.Length);

        Tile[] potentialTiles = GetUnsafeTiles(UnsafeState.None); // get array of unsafe tiles: pinkTiles
        Shuffle(potentialTiles);

        for (int i = 0; i < nextUnsafeCount && i < potentialTiles.Length; i++) {
            potentialTiles[i].SetUnsafeState(UnsafeState.Next);
        }
    }

    public void CheckTransition() {
        if (tiles[4,6].unit.unitType == UnitType.Character && tiles[9,6].unit.unitType == UnitType.Character) {
            print("heh");
            if (((CharacterUnit) tiles[4,6].unit).characterName == "Dursasana" && ((CharacterUnit)tiles[9,6].unit).characterName == "Arjuna") {
                GameManager.Instance.LoadScene("heh");
            }
        }
    }
}
