using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cooldown {

    public static IEnumerator CountDown(Character Player, float posOffset, float scaleOffset) {
        float i = 0.0f;

        //scale diff -0.1818
        //position  diff .49
        Vector3 destination = new Vector3(Player.ActionSlider.transform.position.x + posOffset, Player.ActionSlider.transform.position.y, Player.ActionSlider.transform.position.z);
        Vector3 scaleDestination = new Vector3(Player.ActionSlider.transform.localScale.x + scaleOffset, Player.ActionSlider.transform.localScale.y, Player.ActionSlider.transform.localScale.z);
        while (i < Player.movementCooldown) {
            float deltaTime = Time.deltaTime;
            i += deltaTime;
            Vector3 currentPos = Player.ActionSlider.transform.position;
            float time = Vector3.Distance(currentPos, destination) / (Player.movementCooldown - i) * deltaTime;

            Vector3 currentScale = Player.ActionSlider.transform.localScale;
            float timeScale = Vector3.Distance(currentScale, scaleDestination) / (Player.movementCooldown - i) * deltaTime;

            Player.ActionSlider.transform.position = Vector2.MoveTowards(currentPos, destination, time);
            Player.ActionSlider.transform.localScale = Vector2.MoveTowards(currentScale, scaleDestination, timeScale);
            yield return null;
        }
    }

    public static void ResetPosition(Character Player, float posOffset, float scaleOffset) {
        Player.ActionSlider.transform.position = new Vector3(Player.ActionSlider.transform.position.x - posOffset, Player.ActionSlider.transform.position.y, Player.ActionSlider.transform.position.z);
        Player.ActionSlider.transform.localScale = new Vector3(Player.ActionSlider.transform.localScale.x - scaleOffset, Player.ActionSlider.transform.localScale.y, Player.ActionSlider.transform.localScale.z);
    }
}