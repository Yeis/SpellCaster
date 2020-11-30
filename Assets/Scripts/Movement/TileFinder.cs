using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileFinder : MonoBehaviour {
    public BoundsInt area;
    public Tilemap tileMap;
    // Start is called before the first frame update
    void Start() {
        TileBase[] tileArray = tileMap.GetTilesBlock(area);
        for (int index = 0; index < tileArray.Length; index++) {
            print(tileArray[index]);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(area.position, area.size);
    }
}
