using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : BattleState {
    public CastState(Player player, UIController ui) : base(player, ui) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Cast;
        UserInterface.StateEnum = PlayerState.Cast;

        // TODO - Spell animation
        yield return new WaitForSeconds(1f);

        Player.stockpile = null;
        Player.SetState(new CooldownState(Player, UserInterface));
    }
}
