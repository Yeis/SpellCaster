using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class AttackState : EnemyState {
        public AttackState(Enemy enemy) : base(enemy) { }

        public override IEnumerator Start() {
            Enemy.Animator.SetFloat("Horizontal", Enemy.CurrDirection.x);
            Enemy.Animator.SetFloat("Vertical", Enemy.CurrDirection.y);
            Enemy.CombatController.Attack(Enemy.spells[0], Enemy.CurrDirection);
            yield return new WaitForSeconds(1f);
            Enemy.SetState(new WaitState(Enemy));
        }
    }
}

