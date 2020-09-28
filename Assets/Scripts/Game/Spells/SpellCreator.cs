using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnDistance = 10f;

    public void CreateSpell(GameObject spell, Transform transform, Vector2 direction)
    {
        print("Rotation: " + transform.rotation);
        float angle = Vector2.Angle(Direction.Right ,direction);
        print("Angle:" + angle);
        Instantiate(spell, new Vector2(transform.position.x + (spawnDistance * direction.x),
            transform.position.y + (spawnDistance * direction.y)),
            Quaternion.Euler(0, 0, angle));


        // switch (direction)
        // {
        //     case Direction.Up:
        //         Instantiate(spell, new Vector2(transform.position.x, transform.position.y + spawnDistance), Quaternion.Euler(0, 0, 90));
        //         break;
        //     case Direction.Down:
        //         Instantiate(spell, new Vector2(transform.position.x, transform.position.y - spawnDistance), Quaternion.Euler(0, 0, 270));
        //         break;
        //     case Direction.Left:
        //         Instantiate(spell, new Vector2(transform.position.x - spawnDistance, transform.position.y), Quaternion.Euler(0, 0, 180));
        //         break;
        //     case Direction.Right:
        //         Instantiate(spell, new Vector2(transform.position.x + spawnDistance, transform.position.y), Quaternion.Euler(0, 0, 0));
        //         break;
        //     default:
        //         break;
        // }


        // Instantiate(spell, new Vector2(transform.position.x + spawnDistance, transform.position.y), transform.rotation);
    }

}
