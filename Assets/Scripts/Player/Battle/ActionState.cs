using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : BattleState {
    public ActionState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Action;
        UserInterface.StateEnum = PlayerState.Action;

        yield return WaitForMenuInput();
        // TODO - stockpiling spell if typing is successful 
        Player.stockpile = Player.spellBook[0];
    }

    private IEnumerator WaitForMenuInput() {
        while (Player.StateEnum == PlayerState.Action) {
            if (!UserInterface.IsInAttackMenu && !UserInterface.IsInTypingMode) {
                // animacion de algo? si no esta este wait, y el spell termina en W, A, S, o D, 
                // el maguito inmediatamente se mueve
                yield return new WaitForSeconds(0.5f);
                Player.SetState(new StandbyState(Player));
            }
            yield return null;
        }
    }

}
