using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandbyState : BattleState
{
    public StandbyState(Player player) : base(player) { }

    public virtual IEnumerator Start() {
        yield break;
    }
}
