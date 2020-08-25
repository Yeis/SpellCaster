using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement : MonoBehaviour
{
    public float speed = 3f;
    public bool isForward = true;
    private Rigidbody2D rigidbody;
    public Spell spell;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (isForward) ? Vector2.right : Vector2.left;
        rigidbody.AddForce(direction * speed);
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
