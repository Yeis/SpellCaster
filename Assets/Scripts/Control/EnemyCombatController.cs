using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{

    private bool isAttacking;
    public float timeLeft;
    public float timeBetweenAttacks = 2f;
    private Enemy enemy;
    public Mage mage;
    private EnemyHudController enemyHudController;
    private SpellCreator spellCreator;


    // Start is called before the first frame update
    void Start()
    {
        spellCreator = GetComponent<SpellCreator>();
        enemy = GetComponent<Enemy>();
        isAttacking = true;
        timeLeft = enemy.castingTime;
        enemyHudController = GetComponent<EnemyHudController>();
        enemyHudController.SetupHUD(enemy.castingTime, enemy.health);

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = Mathf.Max(timeLeft - Time.deltaTime, 0);
        enemyHudController.UpdateHealth(enemy.health);
        //Waiting for attack
        if (isAttacking && timeLeft > 0.0)
        {
            enemyHudController.UpdateCastingTime(timeLeft);
        }
        else if (isAttacking && timeLeft == 0.0)
        {
            isAttacking = false;
            enemyHudController.UpdateCastingTime(timeLeft);
            timeLeft = timeBetweenAttacks;
            //Attack
            spellCreator.CreateSpell(transform);
        }
        else if (!isAttacking && timeLeft == 0.0)
        {
            isAttacking = true;
            timeLeft = enemy.castingTime;
            enemyHudController.UpdateCastingTime(timeLeft);


        }
    }
}
