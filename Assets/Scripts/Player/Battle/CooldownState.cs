using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState : BattleState {
    // TODO - handle different types of cooldown, implement Action enum? Move, Cast(SpellCost)
    public CooldownState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Cooldown;
        yield return Cooldown.CountDown(Player, 0.49f, 0.1789f);
        Player.SetState(new StandbyState(Player));
    }
}
