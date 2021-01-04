using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public interface UIControllerListener {
    void wantsToAim();
}

public class StandbyState : BattleState {
    public StandbyState(Player player, UIController ui) : base(player, ui) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Standby;
        UserInterface.StateEnum = PlayerState.Standby;

        // Remove Aiming UI in case we're coming back from the Aim state
        Player.BattleFieldController.RemovePreAttack();

        yield return WaitForMenuInpuOrMovement();
    }

    private IEnumerator WaitForMenuInpuOrMovement() {
        bool hasMoved = false;

        while (Player.StateEnum == PlayerState.Standby && !hasMoved) {

            // Movement
            Vector3 movementVector = new Vector3(0, 0, 0);
            if (Player.MovementInput.x < 0) {
                movementVector = new Vector3(-1, 0, 0);
            } else if (Player.MovementInput.x > 0) {
                movementVector = new Vector3(1, 0, 0);
            } else if (Player.MovementInput.y < 0) {
                movementVector = new Vector3(0, -1);
            } else if (Player.MovementInput.y > 0) {
                movementVector = new Vector3(0, 1);
            }

            if (Player.MovementInput.x != 0 || Player.MovementInput.y != 0) {
                hasMoved = true;
                Player.transform.position += movementVector;
                Player.SetState(new CooldownState(Player, UserInterface));
            }

            // Menu input
            if (UserInterface.IsInAttackMenu) {
                Player.SetState(new AimState(Player, UserInterface));
            }
            yield return null;
        }
    }

}
