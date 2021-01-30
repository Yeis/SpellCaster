using System.Collections.Generic;
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
    public int health = 50;
    public float speed = 5f;
    public Spell stockpile = null;

    public List<Spell> spellBook;
    public GameObject Slider;
    private Scene currentScene;
    private BattleFieldController battleFieldController;
    public BattleFieldController BattleFieldController { get => battleFieldController; set => battleFieldController = value; }
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }
    private PlayerState state = PlayerState.Unknown;
    public PlayerState StateEnum { get => state; set => state = value; }
    private UIController uIController;

    /// Movement 
    private Vector2 movementInput;
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    public bool HasMoved { get => hasMoved; set => hasMoved = value; }
    public bool InBattle { get => inBattle; set => inBattle = value; }

    public Vector3 direction = new Vector3();
    private bool hasMoved, inBattle = false;

    private void Awake() {
        if (GameObject.FindGameObjectsWithTag("BattleField").Length > 0) {
            BattleFieldController = GameObject.FindGameObjectWithTag("BattleField").GetComponent<BattleFieldController>();
        }
        uIController = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UIController>();

        currentScene = SceneManager.GetActiveScene();

        spellBook = new List<Spell>();
        Spell fira = new Spell("fira", 10, 2, 1.5f, SpellType.Fire, Vector2.zero,
            new HashSet<Vector2> { Direction.Left, Direction.Right, Direction.Up, Direction.Down });
        Spell heal = new Spell("curita", -10, 3, 3f, SpellType.Protection, Vector2.zero,
            new HashSet<Vector2> { Direction.Left, Direction.Right });

        spellBook.Add(fira);
        spellBook.Add(heal);

    }

    private void Start() {
        Slider = gameObject.transform.Find("Slider").gameObject;
        ActionSlider = Slider.transform.Find("Action_Mask").gameObject;
        Animator = GetComponent<Animator>();
        //Setup Health bar
        uIController.HPSlider.maxValue = this.health;
        uIController.HPSlider.value = this.health;
        uIController.HPSlider.minValue = 0;
    }

    private void Update() {
        if (InBattle && this.stockpile != null
            && this.BattleFieldController.IsEnemyInRange(this.gameObject, new List<GameObject>(), this.stockpile)) {
            SetState(new CastState(this));
        }
    }

    void FixedUpdate() {
        /// Movement - Should be deprecated by state machine
        if (!InBattle) {
            direction = GetMovementDirection();
            transform.position += direction;
        }
    }

    public string GetCurrentAnimationStateName() {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo[0].clip.name;
    }

    public Vector3 GetDirectionFromAnimationState() {
        string currentClip = GetCurrentAnimationStateName();
        switch (currentClip) {
            case "walking-front":
                return Vector3.down;
            case "walking_back":
                return Vector3.up;
            case "walking_left":
                return Vector3.left;
            case "walking_right":
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }

    public void TakeDamage(int damage) {
        this.health = Mathf.Max(0, this.health - damage);
        uIController.UpdateHealth(this.health, true);
        if(health == 0) {
            print("He muerto");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        transform.position -= direction;
    }

    // Should be deprecated by state machine
    private Vector3 GetMovementDirection() {
        // Check for "empty" inputs so that mage doesn't end up always looking at the same direction
        // This allows for the player to remain in the direction from the last input
        if (!(movementInput.x == 0 && movementInput.y == 0)) {
            Animator.SetBool("Moving", true);
            Animator.SetFloat("Horizontal", movementInput.x);
            Animator.SetFloat("Vertical", movementInput.y);
        } else {
            Animator.SetBool("Moving", false);
        }

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
