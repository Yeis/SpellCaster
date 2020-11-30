using System.Collections;
using UnityEngine;

public class BounceState : BeeState {
    public BounceState(Bee bee) : base(bee) {
    }

    public override IEnumerator Start() {
        Vector3 initialPos = Bee.InitialPosition;
        Bee.InitialPosition = Bee.transform.position;
        Bee.Destination = initialPos;

        Bee.transform.localScale = new Vector3(-1, 1, 1);

        //We will wait 1 frame before starting
        Bee.SetState(new GoState(Bee));
        yield break;
    }
}