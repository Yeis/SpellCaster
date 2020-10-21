using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public string name = "MrYeis";
    public float health = 50;
    public float speed = 5;
    private List<Spell> spellBook;
    private float t;
    private Vector2 currDirectiion;

    public Animator animator;
    void Start()
    {
        spellBook = new List<Spell>();
        // Spell fira = new Spell("fira", 10f, SpellType.Fire);
        // Spell heal = new Spell("curita", -10f, SpellType.Protection);

        // spellBook.Add(fira);
        // spellBook.Add(heal);
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftArrow)) {
        //     currDirectiion = Direction.Left;
        //     animator.SetFloat("Horizontal", 1);
        //     animator.SetFloat("Vertical", 0);
        // }
        // else if (Input.GetKeyDown(KeyCode.RightArrow)) {
        //     currDirectiion = Direction.Right;
        //     animator.SetFloat("Horizontal", -1);
        //     animator.SetFloat("Vertical", 0);

        // } else {
        //     animator.SetFloat("Horizontal", 0);
        //     animator.SetFloat("Vertical", 0);
        //     currDirectiion = Vector2.zero;
        // }

        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        
        transform.position = transform.position + (movement * speed) * Time.deltaTime;

        // t += Time.deltaTime / 3f;
        // transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + currDirectiion, t);



    }
}
