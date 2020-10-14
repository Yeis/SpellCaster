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
    public float maxDistance;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = "searching";
        initialPosition = transform.position;
        isMoving = false;
        animator = GetComponent<Animator>();
        t = 0;

    }

    private void Search()
    {
        if (isMoving)
        {
            t += speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(initialPosition, destination, t);
            if ((Vector2)transform.position == destination)
            {
                isMoving = false;
                animator.SetBool("isMoving", false);

            }
        }
        else
        {
            t = 0;
            isMoving = true;
            initialPosition = transform.position;
            animator.SetBool("isMoving", true);

            Vector2 flowerPosition = ScanAreaForFlowers();
            if (flowerPosition != Vector2.zero)
            {
                destination = flowerPosition;
                print("flowerPosition: " + flowerPosition);
                currentState = "going";
            }
            else
            {
                SetRandomDirection();
            }
        }

    }

    private void SetRandomDirection()
    {
        destination = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));

        //Mirror Sprite logic
        if (destination.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        destination += (Vector2)transform.position;
    }

    private Vector2 ScanAreaForFlowers()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, maxDistance);
        return (collider != null && collider.gameObject.tag == "Flower") ? (Vector2)collider.transform.position : Vector2.zero;
    }

    private void GoToFlower()
    {
        t += speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(initialPosition, destination, t);
        if ((Vector2)transform.position == destination)
        {
            currentState = "pollination";
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case "searching":
                //movement related logic
                Search();
                break;
            case "going":
                GoToFlower();
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
