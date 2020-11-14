using System.Collections;
using UnityEngine;

public class WaitState : EnemyState
{
    public WaitState(Enemy enemy) : base(enemy)
    {
    }
    
    public override IEnumerator Start()
    {
        Enemy.print("WaitState");
        Enemy.animator.SetFloat("Horizontal", Enemy.currDirectiion.x);
        Enemy.animator.SetFloat("Vertical", Enemy.currDirectiion.y);
        yield return new WaitForSeconds(2f);
        Enemy.SetState(new FollowState(Enemy));
    }
}