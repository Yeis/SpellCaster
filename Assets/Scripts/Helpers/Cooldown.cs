using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cooldown {

    public static IEnumerator RestoreEnergy(Character Player, float offset) {
        float i = 0.0f;
        Vector3 destination = new Vector3(Player.ActionSlider.transform.position.x + offset, Player.ActionSlider.transform.position.y, Player.ActionSlider.transform.position.z);
        while (i < Player.movementCooldown) {
            i += Time.deltaTime;
            Vector3 currentPos = Player.ActionSlider.transform.position;
            float time = Vector3.Distance(currentPos, destination) / (Player.movementCooldown - i) * Time.deltaTime; ;
            Player.ActionSlider.transform.position = Vector2.MoveTowards(currentPos, destination, time);
            yield return null;
        }
    }

    public static void SpendEnergy(Character Player, float offset) {
        Player.ActionSlider.transform.position = new Vector3(Player.ActionSlider.transform.position.x - offset, Player.ActionSlider.transform.position.y, Player.ActionSlider.transform.position.z);
    }
}