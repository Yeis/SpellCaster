using System.Collections;
using UnityEngine.InputSystem;
using System.ComponentModel;
using UnityEngine;

public class AimState : BattleState {
    public AimState(Player player, UIController ui) : base(player, ui) { }

    public override IEnumerator Start() {

        Player.StateEnum = PlayerState.Aim;
        UserInterface.StateEnum = PlayerState.Aim;
        yield return WaitForMenuInput();

        // Player has made a choice, erase spell range
        yield return null;
    }

    public override IEnumerator WaitForMenuInput() {
        while (Player.StateEnum == PlayerState.Aim) {
            if (!UserInterface.IsInAttackMenu && !UserInterface.IsInTypingMode) {
                Player.SetState(new StandbyState(Player, UserInterface));
            } else if (UserInterface.IsInTypingMode) {
                Player.SetState(new ActionState(Player, UserInterface));
            }
            yield return null;
        }
    }

    public override void currentSpellChanged(object sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == "CurrentSpell") {
            Player.BattleFieldController.DrawPreAttack(Player.gameObject.transform.Find("PositionReference").gameObject, UserInterface.CurrentSpell);
        }
    }
}
