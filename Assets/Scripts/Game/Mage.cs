using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public string name = "MrYeis";
    public float health = 50;
    private List<Spell> spellBook;
    // Start is called before the first frame update
    void Start()
    {
        spellBook = new List<Spell>();
        // Spell fira = new Spell("fira", 10f, SpellType.Fire);
        // Spell heal = new Spell("curita", -10f, SpellType.Protection);

        // spellBook.Add(fira);
        // spellBook.Add(heal);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
