using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mover {

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

    //This function requires gameObject to have a RigidBody2D
    public static IEnumerator MoveStepWithPhysics(GameObject gameObject, Vector3 destination, float speed) {
        float i = 0.0f;
        float rate = 1.0f / speed;
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
        Vector3 tempPosition = gameObject.transform.position;

        while (i <= 1.0f) {
            i += Time.fixedDeltaTime * rate;
            tempPosition = Vector3.Lerp(gameObject.transform.position, destination, i);
            rg.MovePosition(tempPosition);

            yield return new WaitForEndOfFrame();

        }
        yield return new WaitForEndOfFrame();
    }



    //Careful if object collides with something before getting into the destination it will walk forever
    public static IEnumerator MovePath(GameObject gameObject, Vector3 destination, float speed) {
        float i = 0.0f;
        float rate = 1.0f / speed;
        Vector3 tempPosition = gameObject.transform.position;

        while (gameObject.transform.position != destination) {
            i += Time.deltaTime * rate;
            gameObject.transform.position = Vector2.MoveTowards(tempPosition, destination, i);
            yield return null;
        }
    }
}