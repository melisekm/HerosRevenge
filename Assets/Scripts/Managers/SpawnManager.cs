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
        var player = Instantiate(playerScriptable.prefab, playerSpawn.position, Quaternion.identity);
        // var playerUnit = player.GetComponent<PlayerUnit>();
        // playerUnit.SetAttributes(new Attributes(playerScriptable.attributes));
        // playerUnit.SetStats(new PlayerStats(playerScriptable.playerStats));
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        ScriptableEnemy enemyScriptable = ResourceSystem.Instance.GetRandomEnemy();
        // ScriptableEnemy enemyScriptable = ResourceSystem.Instance.GetEnemyByType(EnemyType.Vermener);
        EnemyUnit enemy = Instantiate(enemyScriptable.prefab, spawnPoint.position, Quaternion.identity) as EnemyUnit;
        if (enemy)
        {
            // do any level based stuff here
            enemy.energyDropAmount = enemyScriptable.energyDropAmount; // * level
            enemy.SetAttributes(new Attributes(enemyScriptable.attributes));
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