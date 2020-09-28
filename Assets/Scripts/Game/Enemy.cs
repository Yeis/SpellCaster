using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public List<GameObject> spells;
    private bool isAttacking;
    private bool isMoving;
    private bool isDead;
    private Vector2 currDirectiion;
    // Start is called before the first frame update
    private CombatController combatController;
    private Animator animator;
    void Start()
    {
        combatController = GetComponent<CombatController>();
        animator = GetComponent<Animator>();
        currDirectiion = Direction.Down;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            combatController.Attack(spells[0], currDirectiion);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            currDirectiion = Direction.BackwardRight;
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            currDirectiion = Direction.ForwardRight;
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            currDirectiion = Direction.BackwardLeft;
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            currDirectiion = Direction.ForwardLeft;
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currDirectiion = Direction.Up;
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 0);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currDirectiion = Direction.Down;
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 0);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currDirectiion = Direction.Left;
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currDirectiion = Direction.Right;
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);

        }
   

        print("CurrDirection: " + currDirectiion);

    }
}
