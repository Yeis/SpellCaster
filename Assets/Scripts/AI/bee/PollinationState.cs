using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollinationState : BeeState
{
    public PollinationState(Bee bee) : base(bee)
    {
    }

    public override IEnumerator Start()
    {
        Bee.print("PollinationState");

        Bee.animator.SetBool("isPollinating", true);
        yield return new WaitForSeconds(2f);
        Bee.animator.SetBool("isPollinating", false);
        Bee.SetState(new SearchState(Bee));
    }
}