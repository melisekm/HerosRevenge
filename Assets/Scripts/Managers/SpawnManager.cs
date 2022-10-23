using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    private readonly List<Transform> spawnPoints = new();
    public float spawnRate = 1f;
    public bool shouldSpawn = true;


    private void Start()
    {
        var spawnPointsGo = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var spawnPoint in spawnPointsGo)
        {
            spawnPoints.Add(spawnPoint.transform);
        }
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        Debug.Log("Spawned at " + spawnPoint.gameObject.name);
        var enemyScriptable = ResourceSystem.Instance.GetRandomEnemy();
        // var enemy = Instantiate()
    }

    public void SpawnEnemies()
    {
        shouldSpawn = true;
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    public void StopSpawning()
    {
        shouldSpawn = false;
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (shouldSpawn)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                SpawnEnemy(spawnPoint);
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}