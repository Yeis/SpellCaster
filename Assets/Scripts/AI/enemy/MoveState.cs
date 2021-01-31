using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class MoveState : EnemyState {

        public MoveState(Enemy enemy) : base(enemy) {
        }

        public override IEnumerator Start() {           
            #region  Random movement 
            // float randomDirection = Random.Range(0,1) > 0.5f? 1.0f: -1.0f;
            // //Character was moving either left or right
            // if(Enemy.CurrDirection.x != 0.0f) {
            //     Enemy.CurrDirection = new Vector2(0.0f, randomDirection);
            // } 
            // //Enemy was moving up or down
            // else {
            //     Enemy.CurrDirection = new Vector2(randomDirection, 0.0f);
            // }
            #endregion

            // Perpendicular Strategy
            Enemy.CurrDirection = Vector2.Perpendicular(Enemy.CurrDirection);
            Vector3 currDirection3d = (Vector3) Enemy.CurrDirection;
            Enemy.destination = Enemy.transform.position + currDirection3d;
 
            // Validate we are not moving into an occupied state
            foreach (GameObject enemy in Enemy.BattleFieldReference.currentEnemies) {
                if (Enemy.name != enemy.name &&
                    Vector3.Distance(Enemy.destination, enemy.transform.position) <= 1 &&
                    Vector3.Distance(Enemy.destination, enemy.GetComponent<Enemy>().destination) <= 1) {
                    Enemy.SetState(new MoveState(Enemy));
                    yield break;
                }
            }

            Enemy.Animator.SetFloat("Horizontal", currDirection3d.x);
            Enemy.Animator.SetFloat("Vertical", currDirection3d.y);
            Enemy.Animator.SetBool("Moving", true);
            yield return new WaitForSeconds(1.0f);
            yield return Mover.MoveStep(Enemy.gameObject, Enemy.destination, Enemy.speed);
            Enemy.Animator.SetBool("Moving", false);
            Enemy.SetState(new WaitState(Enemy));
        }
    }
}

