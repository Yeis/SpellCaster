using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState {
    public AttackState(Enemy enemy) : base(enemy) { }

    public override IEnumerator Start() {
        Enemy.Animator.SetFloat("Horizontal", Enemy.CurrDirectiion.x);
        Enemy.Animator.SetFloat("Vertical", Enemy.CurrDirectiion.y);
        Enemy.CombatController.Attack(Enemy.spells[0], Enemy.CurrDirectiion);
        yield return new WaitForSeconds(2f);

        Enemy.SetState(new WaitState(Enemy));
    }


}