using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public string name;
    public float damage;
    public int maxDistance;
    public float castingTime;
    public SpellType type;
    public Vector2 direction;
    public HashSet<Vector2> validDirections;

    public Vector2 Direction { get => direction; set => direction = value; }

    public Spell(string name, float damage, int maxDistance, float castingTime, SpellType type, Vector2 direction,  HashSet<Vector2> validDirections)
    {
        this.name = name;
        this.damage = damage;
        this.maxDistance = maxDistance;
        this.castingTime = castingTime;
        this.type = type;
        this.Direction = direction;
        this.validDirections = validDirections;
    }
}
