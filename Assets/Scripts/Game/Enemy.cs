using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyStateMachine {
    //Public Fields
    public float health = 30f, speed = 3f, movementCooldown = 2f;
    public List<GameObject> spells;

    //Private Fields
    private CombatController combatController;
    private Vector2 currDirectiion;
    private Animator animator;
    private GameObject playerReference;
    private BattleFieldController battleFieldReference;
    private GameObject actionSlider;

    public CombatController CombatController { get => combatController; set => combatController = value; }
    public Vector2 CurrDirectiion { get => currDirectiion; set => currDirectiion = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public GameObject PlayerReference { get => playerReference; set => playerReference = value; }
    public BattleFieldController BattleFieldReference { get => battleFieldReference; set => battleFieldReference = value; }
    public GameObject ActionSlider { get => actionSlider; set => actionSlider = value; }

    void Start() {
        PlayerReference = GameObject.FindGameObjectWithTag("Player");
        BattleFieldReference = GameObject.FindGameObjectWithTag("BattleField").GetComponent<BattleFieldController>();
        ActionSlider = gameObject.transform.Find("Action_Mask").gameObject;
        CombatController = GetComponent<CombatController>();
        Animator = GetComponent<Animator>();
        CurrDirectiion = Direction.Down;
        SetState(new WaitState(this));
    }
}
