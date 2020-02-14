using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// different type of tiles
public enum TileHighlight
{
    None,
    Safe,
    PotentialUnsafe,
    CurrentUnsafe,
    NextUnsafe
}

public enum UnsafeState
{
    None,
    Current,
    Next
}

public class Tile : MonoBehaviour
{
    public string materialName;
    public bool isSafe;
    public UnsafeState unsafeState;
    public GameUnit unit;     // holds game unit on the tile

    private Renderer renderer;

    //public Tile(string materialName, bool isSafe, GameUnit unit = null) {  // default GameObject parameter is null i.e. when there is no game unit on the tile
    //    this.materialName = materialName;
    //    this.isSafe = isSafe;
    //    this.unsafeState = UnsafeState.None;
    //    this.unit = unit;
    //}

    //public GameObject GenerateVisual(Vector2Int pos)
    //{
    //    // 'y-coordinate' is on z axis
    //    // Quaternion.identity: no rotation
    //    var map = TileMap.Instance;
    //    GameObject go = GameObject.Instantiate(
    //        map.baseTile,
    //        new Vector3(pos.x, 0, pos.y),
    //        Quaternion.identity);
    //    go.GetComponent<TileComponent>().tile = this;
    //    renderer = go.GetComponent<Renderer>();
    //    renderer.materials = BuildMaterial();
    //    return go;
    //}

    //public GameObject GenerateVisual(Vector2Int pos) {
    //    // 'y-coordinate' is on z axis
    //    // Quaternion.identity: no rotation

    //    renderer = go.GetComponent<Renderer>();
    //    renderer.materials = BuildMaterial();
    //    return go;
    //}

    //public void SetAttributes(string materialName, bool isSafe, GameUnit unit = null) {
    //    this.materialName = materialName;
    //    this.isSafe = isSafe;
    //    this.unsafeState = UnsafeState.None;
    //    this.unit = unit;
    //}

    private void Awake() {
        renderer = GetComponent<Renderer>();
    }

    private void Start() {
        renderer.materials = BuildMaterial();
    }

    public Vector2Int GetPos() {
        var pos = transform.position;
        return new Vector2Int((int) pos.x, (int) pos.z);
    }

    private TileHighlight GetHighlight()
    {
        if (isSafe)
        {
            return TileHighlight.None;
        }
        else {
            if (unsafeState == UnsafeState.None)
            {
                return TileHighlight.PotentialUnsafe;
            }
            else if (unsafeState == UnsafeState.Current)
            {
                return TileHighlight.CurrentUnsafe;
            }
            else
            {
                return TileHighlight.NextUnsafe;
            }
        }
    }

    private Material[] BuildMaterial()
    {
        List<Material> materials = new List<Material>(2);
        materials.Add(TileMap.Instance.tileMaterialMap[materialName]);

        TileHighlight highlight = GetHighlight();
        if (highlight != TileHighlight.None)        // if has highlight, add highlight
        {
            materials.Add(TileMap.Instance.tileHighlightMap[highlight]);
        }

        return materials.ToArray();
    }

    public void SetMaterial(string materialName)
    {
        this.materialName = materialName;
        renderer.materials = BuildMaterial();
    }

    public void SetUnsafeState(UnsafeState unsafeState)
    {
        this.unsafeState = unsafeState;
        renderer.materials = BuildMaterial();
    }
}
