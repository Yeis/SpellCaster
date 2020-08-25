using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Spell
{
    public string name;
    public float damage;
    public SpellType type;

    public Spell(string name, float damage, SpellType type)
    {
        this.name = name;
        this.damage = damage;
        this.type = type;
    }
}
