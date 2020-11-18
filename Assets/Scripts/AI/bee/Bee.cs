using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bee : StateMachine
{
//     public string currentState;
//     private BeeState beeState;
//     public Tilemap tileMap;

    //Searching State Properties
    public Vector2 destination, initialPosition;
    public float speed;
    public float maxDistance;
    public bool hasFoundFlower;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        hasFoundFlower = false;
        animator = GetComponent<Animator>();
        SetState(new SearchState(this));
    }

    //Debugging
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(initialPosition, destination);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        print("Coolision Enter");
        SetState(new BounceState(this));
    }
}
