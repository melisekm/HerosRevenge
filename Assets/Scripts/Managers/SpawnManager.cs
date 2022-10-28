using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    private readonly List<Transform> spawnPoints = new();
    private Transform playerSpawn;
    public float spawnRate = 1f;
    private bool shouldSpawn = true;
    public bool isSpawningEnabled = true;


    protected override void Awake()
    {
        base.Awake();
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        var spawnPointsGo = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var spawnPoint in spawnPointsGo)
        {
            spawnPoints.Add(spawnPoint.transform);
        }
    }


    public void SpawnPlayer()
    {
        var playerScriptable = ResourceSystem.Instance.player;
        Instantiate(playerScriptable.prefab, playerSpawn.position, Quaternion.identity);
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        Debug.Log("Spawned at " + spawnPoint.gameObject.name);
        ScriptableEnemy enemyScriptable = ResourceSystem.Instance.GetRandomEnemy();
        EnemyUnit enemy = Instantiate(enemyScriptable.prefab, spawnPoint.position, Quaternion.identity) as EnemyUnit;
        if (enemy)
        {
            // do any level based stuff here
            enemy.SetAttributes(enemyScriptable.attributes);
        }
    }

    public void SpawnEnemies()
    {
        IEnumerator SpawnEnemiesCoroutine()
        {
            while (shouldSpawn)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    if (!shouldSpawn) break;
                    yield return new WaitForSeconds(spawnRate);
                    SpawnEnemy(spawnPoint);
                }
            }
        }

        if (isSpawningEnabled)
        {
            shouldSpawn = true;
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    public void ToggleSpawning()
    {
        shouldSpawn = !shouldSpawn;
        if (shouldSpawn)
        {
            SpawnEnemies();
        }
    }
}