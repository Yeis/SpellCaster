using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState : BattleState {
    // TODO - handle different types of cooldown, implement Action enum? Move, Cast(SpellCost)
    public CooldownState(Player player) : base(player) { }

    public override IEnumerator Start() {
        Player.StateEnum = PlayerState.Cooldown;

        float i = 0.0f;
        Vector3 destination = new Vector3(Player.ActionSlider.transform.position.x + 1.5f, Player.ActionSlider.transform.position.y, Player.ActionSlider.transform.position.z);
        while (i < Player.movementCooldown) {
            i += Time.deltaTime;
            Vector3 currentPos = Player.ActionSlider.transform.position;
            float time = Vector3.Distance(currentPos, destination) / (Player.movementCooldown - i) * Time.deltaTime; ;
            Player.ActionSlider.transform.position = Vector2.MoveTowards(currentPos, destination, time);
            yield return null;
        }

        Player.ActionSlider.transform.position = new Vector3(Player.ActionSlider.transform.position.x - 1.5f, Player.ActionSlider.transform.position.y, Player.ActionSlider.transform.position.z);

        Player.SetState(new StandbyState(Player));
    }
}
