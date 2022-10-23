using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    private readonly List<Transform> spawnPoints = new();
    private Transform playerSpawn;
    public float spawnRate = 1f;
    public bool shouldSpawn = true;


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


    public void SpawnPlayer(float delay = 3f)
    {
        var playerScriptable = ResourceSystem.Instance.player;
        var player = Instantiate(playerScriptable.prefab, playerSpawn.position, Quaternion.identity);
        player.SetStats(playerScriptable.stats);
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        Debug.Log("Spawned at " + spawnPoint.gameObject.name);
        var enemyScriptable = ResourceSystem.Instance.GetRandomEnemy();
        EnemyUnit enemy = Instantiate(enemyScriptable.prefab, spawnPoint.position, Quaternion.identity) as EnemyUnit;
        enemy.SetStats(enemyScriptable.stats);
    }

    public void SpawnEnemies()
    {
        IEnumerator SpawnEnemiesCoroutine()
        {
            while (shouldSpawn)
            {
                foreach (var spawnPoint in spawnPoints.TakeWhile(spawnPoint => shouldSpawn))
                {
                    yield return new WaitForSeconds(spawnRate);
                    SpawnEnemy(spawnPoint);
                }
            }
        }

        shouldSpawn = true;
        StartCoroutine(SpawnEnemiesCoroutine());
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