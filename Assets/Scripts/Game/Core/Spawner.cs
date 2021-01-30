using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public BattleFieldController battleFieldControllerReference;
    // Start is called before the first frame update
    public bool isEnabled = false;
    public int numberOfEnemies;
    private int numberSpawned;
    public float spawnCooldown;
    private float timeSinceLastSpawn;

 
    public void Spawn() {
        GameObject instance = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        battleFieldControllerReference.currentEnemies.Add(instance);
        instance.gameObject.name = instance.gameObject.name + "_" + battleFieldControllerReference.currentEnemies.Count.ToString();
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
                    Spawn();
                }
            } else {
                this.gameObject.SetActive(false);
            }
        }
    }
}
