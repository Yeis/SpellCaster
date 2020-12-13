using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class AimState : BattleState {
    public AimState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Aim;

        // Draw selected spell, for now we will always use fire
        Player.DrawAttackRange(Player.spellBook[0]);

        yield return WaitForPlayerInput(new Key[] { Key.Period, Key.Comma });

        // Player has made a choice, erase spell range

        switch (choice) {
            case PlayerState.Action:
                Player.SetState(new ActionState(Player));
                break;
            case PlayerState.Standby:
                Player.SetState(new StandbyState(Player));
                break;
            default:
                break;
        }

        yield return null;
    }

    public override void SetChoiceTo(Key key) {
        switch (key) {
            case (Key.Period):
                choice = PlayerState.Action;
                break;
            case (Key.Comma):
                choice = PlayerState.Standby;
                break;
        }
    }

}
