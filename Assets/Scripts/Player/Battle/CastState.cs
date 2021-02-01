using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : BattleState {
    public CastState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Cast;
        var attackSpell = Player.stockpile;
        Player.stockpile = new Spell();

        // Face who you're attacking
        Player.Animator.SetBool("Moving", false);
        Player.Animator.SetFloat("Horizontal", Player.direction.x);
        Player.Animator.SetFloat("Vertical", Player.direction.y);

        // Actually attack
        Player.CombatController.Attack(Player.spells[0], Player.direction);
        yield return new WaitForSeconds(1f);

        Player.SetState(new CooldownState(Player));
    }
}
