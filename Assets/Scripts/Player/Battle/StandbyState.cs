using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class StandbyState : BattleState {
    public StandbyState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Standby;
        UserInterface.StateEnum = PlayerState.Standby;

        // Remove Aiming UI in case we're coming back from the Aim state
        Player.BattleFieldController.ClearPreAttack();

        yield return WaitForMenuInpuOrMovement();
    }

    private IEnumerator WaitForMenuInpuOrMovement() {
        bool hasMoved = false;

        while (Player.StateEnum == PlayerState.Standby && !hasMoved) {
            if (Player.MovementInput.x != 0 || Player.MovementInput.y != 0) {
                hasMoved = true;
                Cooldown.ResetPosition(Player, 0.49f, 0.1789f);
                Player.SetState(new MoveState(Player));
            }

            // Menu input
            if (UserInterface.IsInAttackMenu) {
                Player.SetState(new AimState(Player));
            }
            yield return null;
        }
    }
}
