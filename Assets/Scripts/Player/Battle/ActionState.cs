using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : BattleState {
    public ActionState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Action;
        Player.BattleFieldController.ClearPreAttack();
        yield return null;
    }

}
