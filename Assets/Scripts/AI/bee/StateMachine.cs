using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected BeeState BeeState;
    protected Coroutine currentCoroutine;

    public void SetState(BeeState state)
    {
        // StopCoroutine(currentCoroutine);
        StopAllCoroutines();
        BeeState = state;
        currentCoroutine = StartCoroutine(BeeState.Start());
    }
}
