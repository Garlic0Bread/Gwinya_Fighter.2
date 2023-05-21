using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;       // The enemy prefab to spawn
    public float spawnRadius = 10f;      // The radius within which the player triggers enemy spawn
    public float spawnDelay = 5f;        // The delay between enemy spawns
    public Transform playerTransform;    // The player's transform

    private bool canSpawn = true;        // Flag to control enemy spawning

    void Start()
    {
        // Start the spawn coroutine
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            // Check if the player is within the spawn radius
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= spawnRadius && canSpawn)
            {
                // Spawn the enemy
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);

                // Disable spawning until the player moves away
                canSpawn = false;

                // Wait until the player moves away before enabling spawning again
                yield return new WaitForSeconds(2f);

                // Enable spawning again
                canSpawn = true;
            }
        }
    }

}
