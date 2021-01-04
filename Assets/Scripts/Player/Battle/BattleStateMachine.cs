using System.Collections;
using UnityEngine.InputSystem;
using System.ComponentModel;
using UnityEngine;

// TODO: Move this to a more appropriate place?
public class Character : MonoBehaviour {
    private GameObject actionSlider;
    public GameObject ActionSlider { get => actionSlider; set => actionSlider = value; }
    public float movementCooldown = 1f;
}

public class BattleStateMachine : Character {
    BattleState currState;

    public void SetState(BattleState state) {
        StopAllCoroutines();
        currState = state;
        StartCoroutine(currState.Start());
    }

}

public abstract class BattleState {
    protected Player Player;
    protected UIController UserInterface;

    public PlayerState choice = PlayerState.Unknown;

    public BattleState(Player player, UIController ui) {
        Player = player;
        UserInterface = ui;

        UserInterface.PropertyChanged += currentSpellChanged;
    }

    public virtual IEnumerator Start() {
        yield break;
    }

    public virtual void currentSpellChanged(object sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == "CurrentSpell") {
            Player.BattleFieldController.DrawPreAttack(Player.gameObject.transform.Find("PositionReference").gameObject, UserInterface.CurrentSpell);
        }
    }

}
