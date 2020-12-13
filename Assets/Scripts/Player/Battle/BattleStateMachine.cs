using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour {
    BattleState currState;

    public void SetState(BattleState state) {
        StopAllCoroutines();
        currState = state;
        StartCoroutine(currState.Start());
    }

}

public abstract class BattleState {
    protected Player Player;
    public PlayerState choice = PlayerState.Unknown;

    public BattleState(Player player) {
        Player = player;
    }

    public virtual IEnumerator Start() {
        yield break;
    }

    public virtual IEnumerator WaitForPlayerInput(Key[] keys) {
        bool pressed = false;
        while (!pressed) {
            foreach (Key k in keys) {
                if (Keyboard.current[k].wasPressedThisFrame) {
                    pressed = true;
                    SetChoiceTo(k);
                    break;
                }
            }

            yield return null;
        }
    }

    public virtual void SetChoiceTo(Key key) { }


}