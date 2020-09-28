using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellMovement : MonoBehaviour
{
    public float speed = 3f;
    private float t;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public Spell spell;
    public Vector2 initialPosition, destination;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spell = GetComponent<Spell>();
        animator = GetComponent<Animator>();
        //Setup movement
        t = 0;
        initialPosition = transform.position;
        SetDestination();
    }

    void Update()
    {
        t += Time.deltaTime / speed;
        transform.position = Vector2.Lerp(initialPosition, destination, t);


        if ((Vector2)transform.position == destination)
        {
            animator.SetTrigger("Hit");
        }
    }

    // Move based on RigidBody
    // void FixedUpdate()
    // {
    //     // if (initialPosition.)
    //     print(initialPosition);

    //     Vector2 direction = new Vector2();
    //     switch (spell.Direction)
    //     {
    //         case MoveDirection.Up:
    //             direction = Vector2.up;
    //             break;
    //         case MoveDirection.Down:
    //             direction = Vector2.down;
    //             break;
    //         case MoveDirection.Left:
    //             direction = Vector2.left;
    //             break;
    //         case MoveDirection.Right:
    //             direction = Vector2.right;
    //             break;
    //         default:
    //             break;
    //     }
    //     rigidbody.AddForce(direction * speed);
    // }

    public void OnHit()
    {
        Destroy(gameObject);
    }

    private void SetDestination()
    {
        destination = initialPosition + (spell.direction * spell.maxDistance);
        // switch (spell.Direction)
        // {
        //     case MoveDirection.Up:
        //         destination = initialPosition + (Vector2.up * spell.maxDistance);
        //         break;
        //     case MoveDirection.Down:
        //         destination = initialPosition + (Vector2.down * spell.maxDistance);
        //         break;
        //     case MoveDirection.Left:
        //         destination = initialPosition + (Vector2.left * spell.maxDistance);
        //         break;
        //     case MoveDirection.Right:
        //         destination = initialPosition + (Vector2.right * spell.maxDistance);
        //         break;
        //     default:
        //         break;
        // }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Proyectile")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().health -= spell.damage;

        }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Mage>().health -= spell.damage;
        }
        col.gameObject.GetComponent<Animator>().SetTrigger("TakeDamage");

        Destroy(gameObject);
    }
}
