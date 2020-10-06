using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bee : MonoBehaviour
{
    public string currentState;
    private BeeState beeState;
    public Tilemap tileMap;


    //Searching State Properties
    public BoundsInt sightArea;
    private Vector2 destination, initialPosition;
    private float t;
    public float speed;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        currentState = "searching";
        initialPosition = transform.position;
        //5 -2 0
        sightArea = new BoundsInt(new Vector3Int(Mathf.CeilToInt(transform.position.x + 1),
            Mathf.CeilToInt(transform.position.y),
            Mathf.CeilToInt(transform.position.z)),
            //Z component must be at least 1;
            new Vector3Int(2, 2, 1));

    }

    private void Move()
    {
        if (isMoving)
        {
            t += Time.deltaTime / speed;
            transform.position = Vector2.Lerp(initialPosition, destination, t);
        }
        else
        {
            ScanAreaForFlowers();
        }

    }

    private Vector2 ScanAreaForFlowers()
    {
        TileBase[] tileArray = tileMap.GetTilesBlock(sightArea);
        print("TileArray size: " + tileArray.Length);
        for (int index = 0; index < tileArray.Length; index++)
        {
            if(tileArray[index].name.Contains("flower")) {
                return tileArray[index].GetTileData();
            }
            print(tileArray[index]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case "searching":
                //movement related logic
                ScanArea();
                break;
            case "pollination":
                //pollination related logic 
                break;
            default:
                break;
        }
    }

    //Debugging
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(sightArea.position, sightArea.size);
    }
}
