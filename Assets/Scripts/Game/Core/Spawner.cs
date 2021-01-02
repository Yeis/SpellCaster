using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    public void Spawn(){
        Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    }

    private void Start() {
        Spawn();
    }
}
