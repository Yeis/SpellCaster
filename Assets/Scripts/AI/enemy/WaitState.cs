using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class WaitState : EnemyState {

        public WaitState(Enemy enemy) : base(enemy) {
        }

        public override IEnumerator Start() {
            Enemy.Animator.SetFloat("Horizontal", Enemy.CurrDirection.x);
            Enemy.Animator.SetFloat("Vertical", Enemy.CurrDirection.y);
            //scale diff -0.1818
            //position  diff .49
            Cooldown.ResetPosition(Enemy, .49f, -0.1818f);
            yield return new WaitForSeconds(0.2f);
            yield return Cooldown.CountDown(Enemy, .49f, -0.1818f);

            if (InAttackRange()) {
                Enemy.SetState(new AttackState(Enemy));
            } else {
                Enemy.SetState(new FollowState(Enemy));
            }

        }

        private bool InAttackRange() {
            Vector2Int gridPlayerPos = (Vector2Int)Enemy.BattleFieldReference.walkableTileMap.WorldToCell(Enemy.PlayerReference.transform.position);
            Vector2Int gridEnemyPos = (Vector2Int)Enemy.BattleFieldReference.walkableTileMap.WorldToCell(Enemy.transform.position);
            Spell spell = Enemy.spells[0].GetComponent<Spell>();
            int diffPositionX = gridPlayerPos.x - gridEnemyPos.x;
            int diffPositionY = gridPlayerPos.y - gridEnemyPos.y;

            //Check Right
            if (gridPlayerPos.y == gridEnemyPos.y && Mathf.Abs(gridEnemyPos.x - gridPlayerPos.x) <= spell.maxDistance && diffPositionX > 0f) {
                Enemy.CurrDirection = Direction.Right;
                return true;
            }
            //Check Left
            else if (gridPlayerPos.y == gridEnemyPos.y && Mathf.Abs(gridEnemyPos.x - gridPlayerPos.x) <= spell.maxDistance && diffPositionX < 0f) {
                Enemy.CurrDirection = Direction.Left;
                return true;
            }
            //Check Down
            else if (gridPlayerPos.x == gridEnemyPos.x && Mathf.Abs(gridEnemyPos.y - gridPlayerPos.y) <= spell.maxDistance && diffPositionY < 0f) {
                Enemy.CurrDirection = Direction.Down;
                return true;
            }
            //Check Up
            else if (gridPlayerPos.x == gridEnemyPos.x && Mathf.Abs(gridEnemyPos.y - gridPlayerPos.y) <= spell.maxDistance && diffPositionY > 0f) {
                Enemy.CurrDirection = Direction.Up;
                return true;
            }
            return false;
        }
    }
}
