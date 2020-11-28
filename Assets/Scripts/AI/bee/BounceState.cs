using System.Collections;
using UnityEngine;

public class BounceState : BeeState
{
    public BounceState(Bee bee) : base(bee)
    {
    }

    public override IEnumerator Start()
    {
        // Bee.print("BounceState");
        Vector3 initialPos = Bee.initialPosition;
        Bee.initialPosition = Bee.transform.position;
        Bee.destination = initialPos;

        // Bee.print("BounceState InitialPosition: " + Bee.initialPosition);

        // Bee.print("BounceState Destination: " + Bee.destination);
        Bee.transform.localScale = new Vector3(-1, 1, 1);

        // if (Bee.destination.x >= 0)
        // {
        //     Bee.transform.localScale = new Vector3(1, 1, 1);
        // }
        // else
        // {
        //     Bee.transform.localScale = new Vector3(-1, 1, 1);
        // }
        //We will wait 1 frame before starting
        Bee.SetState(new GoState(Bee));
        yield break;



    }
}