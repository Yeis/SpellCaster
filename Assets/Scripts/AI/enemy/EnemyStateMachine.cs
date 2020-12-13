using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {
    EnemyState currState;

    public void SetState(EnemyState state) {
        // StopCoroutine(currentCoroutine);
        StopAllCoroutines();
        currState = state;
        StartCoroutine(currState.Start());
    }

}