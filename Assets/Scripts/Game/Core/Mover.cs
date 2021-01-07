using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mover   {

    public static IEnumerator MoveStep(GameObject gameObject, Vector3 destination, float speed) {
        float i = 0.0f;
        float rate = 1.0f / speed;
        Vector3 tempPosition = gameObject.transform.position;

        while (i <= 1.0f) {
            i += Time.deltaTime * rate;
            gameObject.transform.position = Vector2.MoveTowards(tempPosition, destination, i);
            yield return null;
        }
    }

    //Careful if object collides with something before getting into the destination it will walk forever
    public static IEnumerator MovePath(GameObject gameObject, Vector3 destination, float speed) {
        float i = 0.0f;
        float rate = 1.0f / speed;
        Vector3 tempPosition = gameObject.transform.position;

        while (tempPosition != destination) {
            i += Time.deltaTime * rate;
            gameObject.transform.position = Vector2.MoveTowards(tempPosition, destination, i);
            yield return null;
        }
    }
}