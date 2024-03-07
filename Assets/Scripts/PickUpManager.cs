using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public GameObject pickUpPrefab;
    public Vector3 spawnArea;
    public int maxPickUps = 3;

    void Start()
    {
        for (int i = 0; i < maxPickUps; i++)
        {
            SpawnPickUp();
        }
    }

    void SpawnPickUp()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            // fixed at y = 2
            1,
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );
        Instantiate(pickUpPrefab, randomPosition, Quaternion.identity);
    }

    public void PickUpCollected()
    {
        SpawnPickUp();
    }
}
