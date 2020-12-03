using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bee : BeeStateMachine {
    //Public Fields
    public Animator animator;
    public float speed;
    public float maxDistance;

    //Private fields
    protected Vector2 initialPosition;
    protected bool hasFoundFlower;
    protected Vector2 destination;

    public Vector2 Destination { get => destination; set => destination = value; }
    public Vector2 InitialPosition { get => initialPosition; set => initialPosition = value; }
    public bool HasFoundFlower { get => hasFoundFlower; set => hasFoundFlower = value; }

    // Start is called before the first frame update
    void Start() {
        HasFoundFlower = false;
        animator = GetComponent<Animator>();
        SetState(new SearchState(this));
    }

    //Debugging
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(InitialPosition, Destination);
    }

    void OnCollisionEnter2D(Collision2D col) {
        print("Coolision Enter");
        SetState(new BounceState(this));
    }
}
