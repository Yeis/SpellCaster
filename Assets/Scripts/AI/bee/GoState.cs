using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoState : BeeState {
    public GoState(Bee bee) : base(bee) {
    }

    public override IEnumerator Start() {

        float i = 0.0f;
        float rate = 1.0f / Bee.speed;
        Bee.animator.SetBool("isMoving", true);

        while (i <= 1.0f) {
            i += Time.deltaTime * rate;
            Bee.transform.position = Vector2.MoveTowards(Bee.initialPosition, Bee.destination, i);
            yield return null;
        }

        Bee.animator.SetBool("isMoving", false);

        if (Bee.hasFoundFlower) {
            Bee.SetState(new PollinationState(Bee));
        } else {
            Bee.SetState(new SearchState(Bee));
        }
        yield break;

    }

}