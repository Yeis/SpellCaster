using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : BattleState {
    public ActionState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Action;
        // Remove Aiming UI
        Player.BattleFieldController.RemovePreAttack();

        // TODO - UIController typing, stockpiling spell if typing is successful 
        yield return new WaitForSeconds(1f);

        Player.stockpile = Player.spellBook[0];
        Player.SetState(new CooldownState(Player));
    }
}
