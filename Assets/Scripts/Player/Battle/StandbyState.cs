using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class StandbyState : BattleState {
    public StandbyState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Standby;

        // Remove Aiming UI in case we're coming back from the Aim state
        Player.BattleFieldController.RemovePreAttack();

        yield return WaitForPlayerInput(new Key[] { Key.Z, Key.X, Key.C });

        switch (choice) {
            case PlayerState.Aim:
                Player.SetState(new AimState(Player));
                break;
            case PlayerState.Move:
                Player.SetState(new MoveState(Player));
                break;
            case PlayerState.Cast:
                Player.SetState(new CastState(Player));
                break;
            default:
                break;
        }

        yield return null;
    }

    public override void SetChoiceTo(Key key) {
        switch (key) {
            case (Key.Z):
                choice = PlayerState.Aim;
                break;
            case (Key.X):
                choice = PlayerState.Move;
                break;
            case (Key.C):
                if (!(Player.stockpile is null)) {
                    choice = PlayerState.Cast;
                }
                break;
        }
    }
}
