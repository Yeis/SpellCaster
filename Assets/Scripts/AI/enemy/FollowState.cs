using System.Collections;
using UnityEngine;

public class FollowState : EnemyState
{
    public FollowState(Enemy enemy) : base(enemy)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public void getPathToPlayer(){
        
    }


}