using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : BattleState {
    public MoveState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Move;

        yield return WaitForMovementInput();

        Player.SetState(new CooldownState(Player));
    }

    private IEnumerator WaitForMovementInput() {
        bool hasMoved = false;
        while (!hasMoved) {
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
                Player.Move(movementVector);
            }

            yield return null;
        }
    }
}
