using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyStateMachine
{
    public float health = 30f, speed = 3f, movementCooldown = 2f;
    public List<GameObject> spells;
    public Vector2 currDirectiion;
    // Start is called before the first frame update
    public CombatController combatController;
    public Animator animator;
    public GameObject playerReference;
    public BattleFieldController battleFieldReference;
    public GameObject actionSlider;
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        battleFieldReference = GameObject.FindGameObjectWithTag("BattleField").GetComponent<BattleFieldController>();
        actionSlider = gameObject.transform.Find("Action_Mask").gameObject;
        combatController = GetComponent<CombatController>();
        animator = GetComponent<Animator>();
        currDirectiion = Direction.Down;
        SetState(new WaitState(this));
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         combatController.Attack(spells[0], currDirectiion);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         currDirectiion = Direction.BackwardRight;
    //         animator.SetFloat("Horizontal", -1);
    //         animator.SetFloat("Vertical", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         currDirectiion = Direction.ForwardRight;
    //         animator.SetFloat("Horizontal", -1);
    //         animator.SetFloat("Vertical", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         currDirectiion = Direction.BackwardLeft;
    //         animator.SetFloat("Horizontal", 1);
    //         animator.SetFloat("Vertical", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         currDirectiion = Direction.ForwardLeft;
    //         animator.SetFloat("Horizontal", 1);
    //         animator.SetFloat("Vertical", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.UpArrow))
    //     {
    //         currDirectiion = Direction.Up;
    //         animator.SetFloat("Vertical", 1);
    //         animator.SetFloat("Horizontal", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         currDirectiion = Direction.Down;
    //         animator.SetFloat("Vertical", -1);
    //         animator.SetFloat("Horizontal", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //     {
    //         currDirectiion = Direction.Left;
    //         animator.SetFloat("Horizontal", 1);
    //         animator.SetFloat("Vertical", 0);

    //     }
    //     else if (Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         currDirectiion = Direction.Right;
    //         animator.SetFloat("Horizontal", -1);
    //         animator.SetFloat("Vertical", 0);

    //     }


    //     // print("CurrDirection: " + currDirectiion);

    // }
}
