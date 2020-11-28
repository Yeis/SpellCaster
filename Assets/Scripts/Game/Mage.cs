using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class Mage : MonoBehaviour
{
    /// props
    public string characterName = "MrYeis";
    public float health = 50;
    public float speed = 5;
    private float t = 0;
    public Animator animator;

    /// Private
    public List<Spell> spellBook;
    private Scene currentScene;

    /// movement 
    private Vector2 movementInput;
    private Vector3 direction;
    bool hasMoved;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        spellBook = new List<Spell>();
        Spell fira = new Spell("fira", 10f, 2, 1.5f, SpellType.Fire, Vector2.zero, new HashSet<Vector2>());
        Spell heal = new Spell("curita", -10f, 0, 3f, SpellType.Protection, Vector2.zero, new HashSet<Vector2>());

        spellBook.Add(fira);
        spellBook.Add(heal);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();
        /// Movement
        if (currentScene.name.Contains("Battle"))
        {
            if (movementInput.x == 0 && movementInput.y == 0)
            {
                hasMoved = false;
            }
            else if ((movementInput.x != 0 || movementInput.y != 0) && !hasMoved)
            {
                hasMoved = true;

                direction = GetGridMovementDirection();
            }
        }
        else
        {
            direction = GetMovementDirection();
        }

        transform.position += direction;
    }

    private Vector3 GetMovementDirection()
    {
        print(movementInput.x);

        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);

        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0.0f);
        return (movement * speed) * Time.deltaTime;
    }
    private Vector3 GetGridMovementDirection()
    {
        if (movementInput.x < 0)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (movementInput.x > 0)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (movementInput.y < 0)
        {
            direction = new Vector3(0, -1);
        }
        else if (movementInput.y > 0)
        {
            direction = new Vector3(0, 1);
        }
        return direction;
    }
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position -= direction;
    }

}
