using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyStateMachine {
    public float health = 30f, speed = 3f, movementCooldown = 2f;
    public List<GameObject> spells;
    public Vector2 currDirectiion;
    public CombatController combatController;
    public Animator animator;
    public GameObject playerReference;
    public BattleFieldController battleFieldReference;
    public GameObject actionSlider;

    void Start() {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        battleFieldReference = GameObject.FindGameObjectWithTag("BattleField").GetComponent<BattleFieldController>();
        actionSlider = gameObject.transform.Find("Action_Mask").gameObject;
        combatController = GetComponent<CombatController>();
        animator = GetComponent<Animator>();
        currDirectiion = Direction.Down;
        SetState(new WaitState(this));
    }
}
