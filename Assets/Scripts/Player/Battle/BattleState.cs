using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState {
    protected Player player;

    public BattleState(Player player) {
        this.player = player;
    }

    public virtual IEnumerator Start() {
        yield break;
    }
}
