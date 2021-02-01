using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
public class Spell : MonoBehaviour {

    public string spellName;
    public int damage;
    public int maxDistance;
    public float castingTime;
    public SpellType type;
    public Vector2 direction;
    public HashSet<Vector2> validDirections;

    public Vector2 Direction { get => direction; set => direction = value; }

    public Spell(string name, int damage, int maxDistance, float castingTime, SpellType type, Vector2 direction, HashSet<Vector2> validDirections) {
        this.spellName = name;
        this.damage = damage;
        this.maxDistance = maxDistance;
        this.castingTime = castingTime;
        this.type = type;
        this.Direction = direction;
        this.validDirections = validDirections;
    }

    public Spell() {
        this.spellName = "";
        this.damage = 0;
        this.maxDistance = 0;
        this.castingTime = 0;
        this.type = SpellType.Unknown;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            col.gameObject.GetComponent<Player>().TakeDamage(this.damage);
        } else if (col.tag == "Enemy") {
            col.gameObject.GetComponent<Enemy>().TakeDamage(this.damage);
        }
        //Destroy projectile
        Destroy(this.gameObject);
    }
}
