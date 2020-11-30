using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeeState
{
    protected Bee Bee;
    public BeeState(Bee bee)
    {
        Bee = bee;
    }

    public virtual IEnumerator Start(){
        yield break;
    }

    public virtual IEnumerator Searching() {
        yield break;
    }
}