using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public abstract class EnemyState {
        protected Enemy Enemy;

        public EnemyState(Enemy enemy) {
            Enemy = enemy;
        }

        public virtual IEnumerator Start() {
            yield break;
        }
    }
}

