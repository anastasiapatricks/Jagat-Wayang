
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public string name;
    public Sprite sprite;

    public Item(string name, Sprite sprite) {
        this.name = name;
        this.sprite = sprite;
    }
}

public class Inventory {
    public List<Item> items = new List<Item>();

    public void AddItem(string name) {
        Sprite sprite = UIManager.Instance.itemSpriteMap[name];
        Item item = new Item(name, sprite);

        items.Add(item);
        UIManager.Instance.UpdatePanel(items.ToArray());
    }

    public Item GetItem(string name) {
        return items.Find(item => item.name == name);
    }
}

public class PlayerUnit : MovableGameUnit {
    // private Rigidbody rb;

    // Player movement
    public Vector2Int pos;
    private TileMap map;
    
    public float walkTime = 0.5f;
    private Animator animator;

    public Inventory inventory = new Inventory();

    // Start is called before the first frame update
    void Start()
    {
        map = TileMap.Instance;
        map.SetUnit(this, pos);
        // myRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    public override void Move(Vector2Int newPos) {
        pos = newPos;
        animator.SetBool("isWalking", true);
        LeanTween.move(gameObject, ToWorldPos(newPos), walkTime)
            .setOnComplete(() => animator.SetBool("isWalking", false));
        Camera.main.GetComponent<CameraController>().Adjust(ToWorldPos(newPos));
    }

    private Vector3 ToWorldPos(Vector2Int pos) {
        return new Vector3(pos.x, transform.position.y, pos.y);
    }

    private void UpdateMovement() {
        Vector2Int offset;

        if (Input.GetKey(KeyCode.UpArrow)) {
            offset = new Vector2Int(0, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            offset = new Vector2Int(0, -1);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            offset = new Vector2Int(1, 0);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            offset = new Vector2Int(-1, 0);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        } else {
            return;
        }

        // Don't accept input if player is still moving
        if (LeanTween.isTweening(gameObject))
        {
            return;
        }

        // Check if new position is in the tiles
        var newPos = pos + offset;
        if (!map.IsValidPos(newPos))
        {
            return;
        }

        // Notes: Turn-sequence: Tile - player interaction + AI(safe /unsafe)
        // Player move
        //-updateMoveableObjState(data + visual)(DONE)
        //  if CCmoved-- > check trigger (DONE)
        //  if triggerX, destroy wallX(do Action-- > destroy wall linked to the trigger) (DONE)
        //-isDead ? : destroy gameObject(DONE)
        //- isClear ? : if on ExitTile, check if hasKey, if yes-- > CutsceneEnding (DONE)

        //- randomizeUnsafeTile after end of each turn

        // newPos = where the player is going to move



        GameUnit nextUnit = map.GetUnit(newPos);
        if (nextUnit == null) {
            map.MoveUnit(this, newPos);
        }
        else {
            if (nextUnit.unitType == UnitType.Character || nextUnit.unitType == UnitType.Gunungan) {
                var nextPos = newPos + offset;          // position where CC is being pushed towards
                if (!map.IsValidPos(nextPos) || map.GetUnit(nextPos) != null) {
                    return;
                }

                // valid CCmovement
                map.MoveUnit((MovableGameUnit)nextUnit, nextPos);
                map.MoveUnit(this, newPos);
            }
            else if (nextUnit.unitType == UnitType.Collectible) {
                map.MoveUnit(this, newPos);
                Destroy(nextUnit.gameObject);
                float val = Random.value;
                inventory.AddItem(val < 0.2 ? "wood" : val < 0.4 ? "hide" : val < 0.6 ? "arjuna_blueprint" : val < 0.8 ? "red_paint" : "black_paint");
            } else {
                // Wall in front
                return;
            }
        }


        // Check if is dead
        if (map.GetTile(pos).unsafeState == UnsafeState.Current) {
            Destroy(gameObject);
        }

        // Check if LevelCleared
        Exit exit = map.GetTile(pos).GetComponent<Exit>();
        if (exit != null) {
            exit.LevelCleared();
        }

        TileMap.Instance.NextState();

    }
}
