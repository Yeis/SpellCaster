using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;

public class BattleFieldFactory : MonoBehaviour
{
    public Tilemap mainTilemap;
    public TileBase walkableTile;
    public GameObject UIPrefab;
    private BattleFieldController battleFieldController;
    //All Tilemaps needed;
    private Tilemap walkableTilemap, preAttackTilemap, backgroundTilemap, colliderTilemap, eventsTileMmap;
    private GameObject mainGrid;
    private bool isBattleFieldEnabled;

    private void Start() {
        mainGrid = GameObject.Find("Grid");
        backgroundTilemap = mainGrid.transform.Find("Background_TileMap").gameObject.GetComponent<Tilemap>();
        colliderTilemap = mainGrid.transform.Find("Collider_Tilemap").gameObject.GetComponent<Tilemap>();
        eventsTileMmap = gameObject.GetComponent<Tilemap>();
    }

    public void InitializeBattleField(Vector3Int position, Vector3Int direction){
        //Setup of walkable layer
        if(isBattleFieldEnabled) return;
        isBattleFieldEnabled = true;
        walkableTilemap = createTileMap("Walkable_TileMap", mainGrid, 5);
        preAttackTilemap = createTileMap("PreAttack_TileMap", mainGrid, 5);
  
        Vector3Int startingPosition = (position + direction);
        Stack<Vector3Int> stack = new Stack<Vector3Int>();
        print(startingPosition);
        stack.Push(startingPosition);

        //DFS
        while(stack.Count != 0) {
            Vector3Int currPosition = stack.Pop();
            if(canPlaceTileInPosition(currPosition)){
                //Might change tile 
                preAttackTilemap.SetTile(currPosition, backgroundTilemap.GetTile(currPosition));
                walkableTilemap.SetTile(currPosition, backgroundTilemap.GetTile(currPosition));
            }
    
            //left
            if(canPlaceTileInPosition(currPosition - Vector3Int.left)) {
                stack.Push(currPosition - Vector3Int.left);
            }
            //right
            if (canPlaceTileInPosition(currPosition - Vector3Int.right)) {
                stack.Push(currPosition - Vector3Int.right);
            }
            //up
            if (canPlaceTileInPosition(currPosition - Vector3Int.up)) {
                stack.Push(currPosition - Vector3Int.up);
            }
            //down
            if (canPlaceTileInPosition(currPosition - Vector3Int.down)) {
                stack.Push(currPosition - Vector3Int.down);
            }
        }

        //Setup of BattleFieldController &UI
            GameObject battlefieldGameObject = new GameObject("BattleFieldReference");
            battleFieldController =  battlefieldGameObject.AddComponent<BattleFieldController>();
            battleFieldController.SetupGrid(walkableTilemap, preAttackTilemap, walkableTile);
        
            GameObject UI = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity);
            UI.name = "UI";
    }


    private Tilemap createTileMap(string name, GameObject mainGrid, int sortingOrder) {
        GameObject tileMap = new GameObject(name);
        tileMap.AddComponent<Tilemap>();
        TilemapRenderer tilemapRenderer = tileMap.AddComponent<TilemapRenderer>();
        tilemapRenderer.sortingOrder = sortingOrder;
        tileMap.transform.SetParent(mainGrid.transform);
        return tileMap.GetComponent<Tilemap>();
    }


    private GameObject createWalkableLayer(GameObject mainGrid) {
        GameObject walkableTilemap = new GameObject("Walkable_TileMap");
        walkableTilemap.AddComponent<Tilemap>();
        TilemapRenderer tilemapRenderer = walkableTilemap.AddComponent<TilemapRenderer>();
        tilemapRenderer.sortingOrder = 2;
        walkableTilemap.transform.SetParent(mainGrid.transform);
        return walkableTilemap;
    }

    private bool canPlaceTileInPosition(Vector3Int position) {
        return backgroundTilemap.HasTile(position) && !colliderTilemap.HasTile(position) && !walkableTilemap.HasTile(position) && !eventsTileMmap.HasTile(position);
    }



    private void OnTriggerEnter2D(Collider2D other) {
        //Calculate collision direction
        Player player = other.gameObject.GetComponent<Player>();
        player.InBattle = false;
        //Directio is comes in .1 intervals
        Vector3Int colDirection = Vector3Int.RoundToInt(player.direction * 20);
        InitializeBattleField(Vector3Int.FloorToInt(other.transform.position), colDirection);

        StartCoroutine(InitializePlayerPositions(player.gameObject, colDirection));
    }

    
    //TODO: Based this from Maguito State Machine
    private IEnumerator InitializePlayerPositions(GameObject player, Vector3Int direction){
        // Move past event zone to be inside battle Walkable tile map layer
        float battleSetupMovementSpeed = 2.0f;
        Vector3Int floorPosition = Vector3Int.FloorToInt(player.transform.position);
        Vector3 destination = floorPosition + direction;
        yield return Mover.MovePath(player, destination, battleSetupMovementSpeed);
    }
}
