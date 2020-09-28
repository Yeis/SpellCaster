using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Animator animator;
    private SpellCreator spellCreator;
    // Start is called before the first frame update
    void Start()
    {
        spellCreator = GetComponent<SpellCreator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Sets Enemy trigger and creates spell
    public void Attack(GameObject spell, Vector2 direction)
    {
        animator.SetTrigger("Attack");
        spell.GetComponent<Spell>().Direction = direction;
        spellCreator.CreateSpell(spell, transform, direction);

    }
}
