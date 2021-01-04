﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum PlayerState {
    Standby,
    Cooldown,
    Aim,
    Action,
    Move,
    Cast,
    Unknown
}

public class Player : BattleStateMachine {
    public string characterName = "MrYeis";
    public float health = 50, speed = 5f;
    public Spell stockpile = null;
    public UIController ui;

    public List<Spell> spellBook;
    private Scene currentScene;
    private BattleFieldController battleFieldController;
    public BattleFieldController BattleFieldController { get => battleFieldController; set => battleFieldController = value; }
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }
    private PlayerState state = PlayerState.Unknown;
    public PlayerState StateEnum { get => state; set => state = value; }

    /// Movement 
    private Vector2 movementInput;
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    public Vector3 direction = new Vector3();
    private bool hasMoved;

    private void Awake() {
        if (GameObject.FindGameObjectsWithTag("BattleField").Length > 0) {
            BattleFieldController = GameObject.FindGameObjectWithTag("BattleField").GetComponent<BattleFieldController>();
        }
        currentScene = SceneManager.GetActiveScene();

        spellBook = new List<Spell>();
        Spell fira = new Spell("fira", 10f, 2, 1.5f, SpellType.Fire, Vector2.zero,
            new HashSet<Vector2> { Direction.Left, Direction.Right, Direction.Up, Direction.Down });
        Spell heal = new Spell("curita", -10f, 0, 3f, SpellType.Protection, Vector2.zero, new HashSet<Vector2>());

        spellBook.Add(fira);
        spellBook.Add(heal);

    }

    private void Start() {
        ActionSlider = gameObject.transform.Find("Action_Mask").gameObject;
        Animator = GetComponent<Animator>();

        SetState(new CooldownState(this, ui));
    }

    void FixedUpdate() {
        /// Movement - Should be deprecated by state machine
        if (!currentScene.name.Contains("Battle")) {
            direction = GetMovementDirection();
        }

        transform.position += direction;
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        // transform.position -= direction;
    }

    // Should be deprecated by state machine
    private Vector3 GetMovementDirection() {
        Animator.SetFloat("Horizontal", movementInput.x);
        Animator.SetFloat("Vertical", movementInput.y);

        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0.0f);
        return (movement * speed) * Time.deltaTime;
    }

    public void OnMove(InputValue value) {
        movementInput = value.Get<Vector2>();
    }

    #region Debug GUI
    public GUISkin customGUISkin;

    private void OnGUI() {
        GUI.skin = customGUISkin;
        GUI.Label(new Rect(10, 10, 400, 50), "Current State: " + state);
    }
    #endregion
}
