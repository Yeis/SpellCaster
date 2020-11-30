using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    BattleState currState;

    public void SetState(BattleState state)
    {
        StopAllCoroutines();
        currState = state;
        StartCoroutine(currState.Start());
    }

}
