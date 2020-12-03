using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreator : MonoBehaviour {
    // Start is called before the first frame update
    public float spawnDistance = 10f;

    public void CreateSpell(GameObject spell, Transform transform, Vector2 direction) {
        float zAngle = Vector2.SignedAngle(Direction.Right, direction);

        print("Angle:" + zAngle);
        Instantiate(spell, new Vector2(transform.position.x + (spawnDistance * direction.x),
            transform.position.y + (spawnDistance * direction.y)),
            Quaternion.Euler(0, 0, zAngle));
        // Quaternion.Euler((Mathf.Abs(zAngle) > 90 ? 180 : 0), 0, zAngle));
    }

}
