using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatController : MonoBehaviour
{
    public float castingTime = 5f, timeLeft;
    public float penaltyDuration = 3f, penaltyTimeLeft;
    public Enemy enemy;
    private Mage mage;
    private bool isAttacking = false, canAttack = true;
    private Spell currentSpell;
    private int currentSpellIndex;
    private UIController uIController;
    private SpellCreator spellCreator;


    private void Start()
    {
        mage = GetComponent<Mage>();
        uIController = GetComponent<UIController>();
        spellCreator = GetComponent<SpellCreator>();
        timeLeft = castingTime;
        penaltyTimeLeft = penaltyDuration;
        uIController.SetupPlayerHUD(mage.health);
    }

    private void Update()
    {
        //Always check for player's health
        uIController.UpdateHealth(mage.health);
        //We are attacking
        if (isAttacking && timeLeft > 0.0f)
        {
            timeLeft = Mathf.Max(timeLeft - Time.deltaTime, 0);
            uIController.UpdateTimeLeft(!canAttack, timeLeft);

            //we are currently attacking so we check if there is an input 
            if (Input.inputString.Length > 0)
            {
                foreach (char letter in Input.inputString)
                {
                    //Correct Letter
                    if (currentSpell.name.Length > currentSpellIndex && currentSpell.name[currentSpellIndex] == letter)
                    {
                        uIController.UpdateSpellText(currentSpell.name, ++currentSpellIndex);
                        if (currentSpellIndex == currentSpell.name.Length)
                        {
                            EndAttack();
                            break;
                        }

                    }
                    //Incorrect letter
                    else if (currentSpell.name.Length > currentSpellIndex && currentSpell.name[currentSpellIndex] != letter)
                    {
                        PenalizeAttack();
                        break;
                    }
                }
            }
        }
        //Attack failed penalty duration
        else if (isAttacking && timeLeft == 0.0f)
        {
            PenalizeAttack();
        }
        //We are on a penalty state
        if (!canAttack && penaltyTimeLeft > 0.0f)
        {
            penaltyTimeLeft = Mathf.Max(penaltyTimeLeft - Time.deltaTime, 0);
            uIController.UpdateTimeLeft(!canAttack, penaltyTimeLeft);

        }
        //The penalty is over
        else if (!canAttack && penaltyTimeLeft == 0.0f)
        {
            EndPenalty();
        }
    }

    private void PenalizeAttack()
    {
        isAttacking = false;
        canAttack = false;
        uIController.DisableAttackUI();
        uIController.UpdateSlider(!canAttack, penaltyDuration);
        uIController.UpdateSpellText("Under Penalty", 0);
        uIController.UpdateTimeLeft(!canAttack, timeLeft);
    }

    private void EndPenalty()
    {
        canAttack = true;
        isAttacking = false;
        uIController.EnableAttackUI();
    }

    public void EndAttack()
    {
        isAttacking = false;
        uIController.UpdateSpellText("Spell Successful", 0);

        //Create Attack
        if (currentSpell.type == SpellType.Fire)
        {
            spellCreator.CreateSpell(transform);

        }
        else if (currentSpell.type == SpellType.Protection)
        {
            GetComponent<Animator>().SetTrigger("Heal");
            mage.health += currentSpell.damage;
        }
    }

    public void StartAttack(Spell spell)
    {
        if (canAttack)
        {
            currentSpell = spell;
            timeLeft = castingTime;
            isAttacking = true;
            currentSpellIndex = 0;
            uIController.UpdateSpellText(currentSpell.name, currentSpellIndex);
            uIController.UpdateTimeLeft(!canAttack, timeLeft);
            uIController.UpdateSlider(!canAttack, timeLeft);
        }
    }

    public void AttackCommand()
    {
        if (!isAttacking)
        {
            Spell fira = new Spell("fira", 10f, SpellType.Fire);
            StartAttack(fira);
        }
    }

    public void HealCommand()
    {
        if (!isAttacking)
        {
            Spell heal = new Spell("paracetamol", 20f, SpellType.Protection);
            StartAttack(heal);
        }
    }
}
