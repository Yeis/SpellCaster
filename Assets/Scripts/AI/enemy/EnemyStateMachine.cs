using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class EnemyStateMachine : Character {
        EnemyState currState;

        public void SetState(EnemyState state) {
            // StopCoroutine(currentCoroutine);
            StopAllCoroutines();
            currState = state;
            StartCoroutine(currState.Start());
        }
    }
}

