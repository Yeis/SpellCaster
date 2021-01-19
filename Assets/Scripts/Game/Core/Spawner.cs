using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    public bool isEnabled = false;
    public int numberOfEnemies;
    private int numberSpawned;
    public float spawnCooldown;
    private float timeSinceLastSpawn;

 
    public void Spawn(){
        Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        numberSpawned++;
        timeSinceLastSpawn = 0.0f;
    }

    private void Start() {
        timeSinceLastSpawn = spawnCooldown;
        numberSpawned = 0;
    }

    private void Update()
    {
        if(isEnabled) {
            if (numberSpawned < numberOfEnemies) {
                timeSinceLastSpawn += Time.deltaTime;
                if (timeSinceLastSpawn >= spawnCooldown) {
                    print("Spawning");
                    Spawn();
                }
            } else {
                this.gameObject.SetActive(false);
            }
        }
    }
}
