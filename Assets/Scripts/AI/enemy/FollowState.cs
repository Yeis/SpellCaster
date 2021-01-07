using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState {
    public FollowState(Enemy enemy) : base(enemy) {
    }

    public override IEnumerator Start() {
        //Todo Optimize 
        //Get GridPosition
        Vector2Int gridPlayerPos = (Vector2Int)Enemy.BattleFieldReference.walkableTileMap.WorldToCell(Enemy.PlayerReference.transform.position);
        Vector2Int gridEnemyPos = (Vector2Int)Enemy.BattleFieldReference.walkableTileMap.WorldToCell(Enemy.transform.position);
        List<Spot> path = Enemy.BattleFieldReference.astar.CreatePath(Enemy.BattleFieldReference.spots, gridEnemyPos, gridPlayerPos, 1000);

        #region multiple steps in the same State
        //Movement logic taking a step evey second.

        // for (int j = path.Count - 1; j >= 0; j--)
        // {
        //     i = 0.0f;
        //     Vector3Int floorPosition = Vector3Int.FloorToInt(Enemy.transform.position);
        //     Vector3 tempPosition = Enemy.transform.position;
        //     Vector3 offsetPostion =  tempPosition - floorPosition;
        //     while (i <= 1.0f)
        //     {
        //         i += Time.deltaTime * rate;
        //         Enemy.transform.position = Vector2.MoveTowards(tempPosition, new Vector2(path[j].X + offsetPostion.x, path[j].Y + offsetPostion.y), i);
        //         yield return null;
        //     }
        //     //Wait one second between movement
        //     Enemy.print("J:" + j);
        //     yield return new WaitForSeconds(1f);
        // }
        #endregion

        #region 1 Step per State
        Vector3 offsetPostion = Enemy.transform.position - Vector3Int.FloorToInt(Enemy.transform.position);
        Vector3 destination = new Vector2(path[path.Count - 2].X + offsetPostion.x, path[path.Count - 2].Y + offsetPostion.y);
        Enemy.CurrDirection = destination - Enemy.transform.position;
        Enemy.Animator.SetFloat("Horizontal", Enemy.CurrDirection.x);
        Enemy.Animator.SetFloat("Vertical", Enemy.CurrDirection.y);
        Enemy.Animator.SetBool("Moving", true);
        yield return Mover.MoveStep(Enemy.gameObject, destination, Enemy.speed);
        Enemy.Animator.SetBool("Moving", false);
        Enemy.SetState(new WaitState(Enemy));
        #endregion
    }
}