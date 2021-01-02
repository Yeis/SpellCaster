using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleFieldController : MonoBehaviour {

    GameObject player, enemy;
    public Tilemap walkableTileMap;
    public Tilemap preAttackTileMap;

    public TileBase preAttackTile;
    public Vector3Int[,] spots;
    new Camera camera;
    public BoundsInt bounds;
    public Astar astar;
    List<Spot> roadPath = new List<Spot>();
    public Vector2Int start;

    public void SetupGrid(Tilemap walkableTileMap, Tilemap roadTileMap, TileBase preAttackTile)
    {
        this.walkableTileMap = walkableTileMap;
        this.preAttackTileMap = roadTileMap;
        this.preAttackTile = preAttackTile;
    }
    
    // Start is called before the first frame update
    void Start() {
        walkableTileMap.CompressBounds();
        preAttackTileMap.CompressBounds();
        bounds = walkableTileMap.cellBounds;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        CreateGrid();
        astar = new Astar(spots, bounds.size.x, bounds.size.y);
    }

    public void CreateGrid() {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];

        for (int x = bounds.xMin, i = 0; i < bounds.size.x; x++, i++) {
            for (int y = bounds.yMin, j = 0; j < bounds.size.y; y++, j++) {

                if (walkableTileMap.HasTile(new Vector3Int(x, y, 0))) {
                    spots[i, j] = new Vector3Int(x, y, 0);
                } else {
                    spots[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }

    //Agarra las direcciones del Spell y en base a es pinta los tiles de roadTileMap TileMap
    public void DrawPreAttack(GameObject reference, Spell spell) {
        Vector3Int gridReferencePos = walkableTileMap.WorldToCell(reference.transform.position);
        foreach (Vector2 direction in spell.validDirections) {
            for (int i = 0; i <= spell.maxDistance; i++) {
                preAttackTileMap.SetTile(new Vector3Int(gridReferencePos.x + (i * (int)direction.x) , gridReferencePos.y + (i * (int)direction.y) , 0), preAttackTile);
            }
        }
    }

    public void RemovePreAttack() {
        // TODO
    }

    // Update is called once per frame
    void Update() {
        Vector3Int gridPlayerPos = walkableTileMap.WorldToCell(player.transform.position);
        Vector3Int gridEnemyPos = walkableTileMap.WorldToCell(enemy.transform.position);

        roadPath = astar.CreatePath(spots, new Vector2Int(gridPlayerPos.x, gridPlayerPos.y),
        new Vector2Int(gridEnemyPos.x, gridEnemyPos.y), 1000);
    }
}
