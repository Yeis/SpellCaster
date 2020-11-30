using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeeStateMachine : MonoBehaviour {
    protected BeeState BeeState;
    protected Coroutine currentCoroutine;

    public void SetState(BeeState state) {
        StopAllCoroutines();
        BeeState = state;
        currentCoroutine = StartCoroutine(BeeState.Start());
    }
}
