using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class StandbyState : BattleState {
    public StandbyState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Standby;
        // Remove Aiming UI in case we're coming back from the Aim state
        Player.BattleFieldController.ClearPreAttack();

        yield return WaitForMovement();
    }

    private IEnumerator WaitForMovement() {
        bool hasMoved = false;

        while (Player.StateEnum == PlayerState.Standby && !hasMoved) {
            if (Player.MovementInput.x != 0 || Player.MovementInput.y != 0) {
                hasMoved = true;
                Cooldown.SpendEnergy(Player, 1.5f);
                Player.SetState(new MoveState(Player));
            }
            yield return null;
        }
    }

}
