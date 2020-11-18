using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy enemy) : base(enemy)
    {

    }

    public override IEnumerator Start()
    {
        Enemy.animator.SetFloat("Horizontal", Enemy.currDirectiion.x);
        Enemy.animator.SetFloat("Vertical", Enemy.currDirectiion.y);
        Enemy.combatController.Attack(Enemy.spells[0], Enemy.currDirectiion);
        yield return new WaitForSeconds(2f);

        Enemy.SetState(new WaitState(Enemy));
    }


}