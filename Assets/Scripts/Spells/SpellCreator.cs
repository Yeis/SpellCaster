using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spell;
    public float spawnDistance = 10f;

    public void CreateSpell(Transform transform) {
            Instantiate(spell, new Vector2(transform.position.x+ spawnDistance, transform.position.y) , transform.rotation);
    }

}
