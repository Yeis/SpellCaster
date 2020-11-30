using System.Collections;
using UnityEngine;

public class BounceState : BeeState {
    public BounceState(Bee bee) : base(bee) {
    }

    public override IEnumerator Start() {
        // Bee.print("BounceState");
        Vector3 initialPos = Bee.initialPosition;
        Bee.initialPosition = Bee.transform.position;
        Bee.destination = initialPos;

        Bee.transform.localScale = new Vector3(-1, 1, 1);

        //We will wait 1 frame before starting
        Bee.SetState(new GoState(Bee));
        yield break;



    }
}