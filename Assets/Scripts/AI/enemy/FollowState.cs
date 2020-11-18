using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    public FollowState(Enemy enemy) : base(enemy)
    {
    }

    public override IEnumerator Start()
    {
        //Todo Optimize 

        Enemy.print("FollowState");
 
        //Get GridPosition
        Vector2Int gridPlayerPos = (Vector2Int)Enemy.battleFieldReference.walkableTileMap.WorldToCell(Enemy.playerReference.transform.position);
        Vector2Int gridEnemyPos = (Vector2Int)Enemy.battleFieldReference.walkableTileMap.WorldToCell(Enemy.transform.position);
        List<Spot> path = Enemy.battleFieldReference.astar.CreatePath(Enemy.battleFieldReference.spots, gridEnemyPos, gridPlayerPos, 1000);
        float i = 0.0f;
        float rate = 1.0f / Enemy.speed;

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
        Vector3 tempPosition = Enemy.transform.position;
        Vector3 offsetPostion = tempPosition - Vector3Int.FloorToInt(Enemy.transform.position);
        Vector3 destination = new Vector2(path[path.Count - 2].X + offsetPostion.x, path[path.Count - 2].Y + offsetPostion.y);
        Enemy.currDirectiion = destination - tempPosition;
        Enemy.animator.SetFloat("Horizontal", Enemy.currDirectiion.x);
        Enemy.animator.SetFloat("Vertical", Enemy.currDirectiion.y);
        Enemy.animator.SetBool("Moving", true);


        while (i <= 1.0f)
        {
            i += Time.deltaTime * rate;
            Enemy.transform.position = Vector2.MoveTowards(tempPosition, destination, i);
            yield return null;
        }
        Enemy.animator.SetBool("Moving", false);
        Enemy.SetState(new WaitState(Enemy));
        #endregion
    }




}