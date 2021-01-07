using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BattleState {

    public MoveState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Move;
        UserInterface.StateEnum = PlayerState.Move;

        yield return MovePlayer();
        Player.SetState(new CooldownState(Player));
    }

    private IEnumerator MovePlayer() {
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

        Player.transform.position += movementVector;
        yield return null;
    }

}
