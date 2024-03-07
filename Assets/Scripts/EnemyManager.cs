using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the inspector
    public Vector3 spawnArea; // Defines the area in which the enemy can spawn
    public int maxEnemies = 5; // The maximum number of enemies to spawn
    public int respawnTime = 3;
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {   
        for (int i = 0; i < maxEnemies; i++)
        {
            Invoke("SpawnEnemy", respawnTime);
        }
    }

    public void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            0, // Adjust this if your enemies are not grounded
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );
        GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        enemies.Add(enemy);
    }

    public void EnemyKilled(GameObject killedEnemy)
    {
        enemies.Remove(killedEnemy);
        Invoke("SpawnEnemy", respawnTime);
    }
}
