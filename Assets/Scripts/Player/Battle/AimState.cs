using System.Collections;
using UnityEngine.InputSystem;
using System.ComponentModel;
using UnityEngine;

public class AimState : BattleState {
    public AimState(Player player) : base(player) { }

    public override IEnumerator Start() {

        Player.StateEnum = PlayerState.Aim;
        yield return null;
    }

    public override void currentSpellChanged(object sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == "CurrentSpell") {
            Player.BattleFieldController.ClearPreAttack();
            Player.BattleFieldController.DrawPreAttack(Player.gameObject.transform.Find("PositionReference").gameObject, UserInterface.CurrentSpell);
        }
    }
}
