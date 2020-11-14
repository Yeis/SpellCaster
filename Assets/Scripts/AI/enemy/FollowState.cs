using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    private Astar astar;
    public FollowState(Enemy enemy) : base(enemy)
    {
    }

    public override IEnumerator Start()
    {
        Enemy.print("FollowState");
        //Todo Optimize 
        astar = new Astar(Enemy.battleFieldReference.spots, Enemy.battleFieldReference.bounds.size.x,
            Enemy.battleFieldReference.bounds.size.y);

        //Get GridPosition
        Vector2Int gridPlayerPos = (Vector2Int)Enemy.battleFieldReference.walkableTileMap.WorldToCell(Enemy.playerReference.transform.position);
        Vector2Int gridEnemyPos = (Vector2Int)Enemy.battleFieldReference.walkableTileMap.WorldToCell(Enemy.transform.position);
        List<Spot> path = astar.CreatePath(Enemy.battleFieldReference.spots, gridEnemyPos, gridPlayerPos, 1000);

        //Movement Logic
        float i = 0.0f;
        float rate = 1.0f / Enemy.speed;
        for (int j = 0; j < path.Count; j++)
        {
            Vector3 tempPosition = Enemy.transform.position;
            while (i <= 1.0f)
            {
                i += Time.deltaTime * rate;
                Enemy.transform.position = Vector2.MoveTowards(tempPosition, new Vector2(path[j].X, path[j].Y), i);
                yield return null;
            }
            //Wait one second between movement
            yield return new WaitForSeconds(1f);

        }
        Enemy.SetState(new WaitState(Enemy));
    }




}