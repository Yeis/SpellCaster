using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleFieldController : MonoBehaviour
{

    GameObject player;
    public int scanArea = 5;
    public Tilemap walkableTileMap;
    public Tilemap roadTileMap;
    public Vector3Int[,] spots;
    BoundsInt bounds;

    // Start is called before the first frame update
    void Start()
    {
        walkableTileMap.CompressBounds();
        roadTileMap.CompressBounds();
        bounds = walkableTileMap.cellBounds;
        player = GameObject.FindGameObjectWithTag("Player");
        CreateGrid();
    }

    public void CreateGrid()
    {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];

        for (int x = bounds.xMin, i = 0; x < bounds.size.x; x++, i++)
        {
            for (int y = bounds.yMin, j = 0; x < bounds.size.y; y++, j++)
            {

                if (walkableTileMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    spots[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    spots[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 using UnityEngine;
            using UnityEditor;
            using UnityEditorInternal;
            
            [CustomEditor(typeof(+))]
            public class BattleFieldController : Editor {
                private SerializedProperty _property;
                private ReorderableList _list;
            
                private void OnEnable() {
                    _property = serializedObject.FindProperty("");
                    _list = new ReorderableList(serializedObject, _property, true, true, true, true) {
                        drawHeaderCallback = DrawListHeader,
                        drawElementCallback = DrawListElement
                    };
                }
            
                private void DrawListHeader(Rect rect) {
                    GUI.Label(rect, "");
                }
            
                private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused) {
                    var item = _property.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, item);
                    
                }
            
                public override void OnInspectorGUI() {
                    serializedObject.Update();
                    EditorGUILayout.Space();
                    _list.DoLayoutList();
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }

    public void SetupBattleField()
    {
        float maxDistance = GetFarthestEnemyDistance();
        Stack stack = new Stack();
        //Assumption when we start the scan the player position is on something wakable
        // TileBase tb = obstacles.GetTile(new Vector3Int((int)player.transform.position.x, (int)player.transform.position.y, 0));
    }

    public float GetFarthestEnemyDistance()
    {
        float farthestEnemyDistance = 0f;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance((Vector2)player.transform.position, (Vector2)enemy.transform.position);
            farthestEnemyDistance = Mathf.Max(farthestEnemyDistance, distance);
        }
        return farthestEnemyDistance;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bounds.position, bounds.size);
    }
}
