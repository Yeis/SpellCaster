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
            movementVector = Direction.Left;
        } else if (Player.MovementInput.x > 0) {
            movementVector = Direction.Right;
        } else if (Player.MovementInput.y < 0) {
            movementVector = Direction.Down;
        } else if (Player.MovementInput.y > 0) {
            movementVector = Direction.Up;
        }

        //Animation
        Player.Animator.SetBool("Moving", true);
        Player.Animator.SetFloat("Horizontal", movementVector.x);
        Player.Animator.SetFloat("Vertical", movementVector.y);

        //We need to move player with MovePosition to ensure its collides correctly with TileMaps
        //The issues is that we have to manually move the slider.
        Vector3 destination = Player.transform.position + movementVector;
        yield return Mover.MoveStepWithPhysics(Player.gameObject, destination, 1.0f);
        Player.Animator.SetBool("Moving", false);
        yield return null;
    }

}
