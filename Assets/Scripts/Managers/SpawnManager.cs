using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    private readonly List<Transform> spawnPoints = new();
    private Transform playerSpawn;
    public float spawnRate = 1f;
    public bool shouldSpawn = true;
    public EnemyType spawnType;
    public bool spawnRandomEnemyTypes = true;
    public bool dynamicSpawnRate = true;

    public static event Action<EnemyUnit> OnEnemySpawned;


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

    private void OnEnable()
    {
        EnemyUnit.OnEnemyUnitDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        EnemyUnit.OnEnemyUnitDied -= OnEnemyDied;
    }

    private void OnEnemyDied(EnemyUnit obj)
    {
        DecideSpawnRate();
    }


    public void SpawnPlayer()
    {
        var playerScriptable = ResourceSystem.Instance.player;
        var playerContainer = SceneSystem.Instance.playerContainer;
        var player = Instantiate(playerScriptable.prefab, playerSpawn.position, Quaternion.identity);
        var playerUnit = player.GetComponent<PlayerUnit>();
        playerUnit.SetAttributes(playerContainer.playerAttributes);
        playerUnit.SetStats(playerContainer.playerStats);
        // GameManager.Instance.player = player.gameObject;
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        ScriptableEnemy enemyScriptable;
        if (spawnRandomEnemyTypes)
            enemyScriptable = ResourceSystem.Instance.GetRandomEnemy();
        else
            enemyScriptable = ResourceSystem.Instance.GetEnemyByType(spawnType);

        EnemyUnit enemy = Instantiate(enemyScriptable.prefab, spawnPoint.position, Quaternion.identity) as EnemyUnit;
        if (enemy)
        {
            // do any level based stuff here
            enemy.energyDropAmount = enemyScriptable.energyDropAmount; // * level
            enemy.SetAttributes(new Attributes(enemyScriptable.attributes));
            Debug.Log("Spawned enemy: " + enemy.name + " with HP: " + enemy.attributes.health.actual);
            OnEnemySpawned?.Invoke(enemy);
            DecideSpawnRate();
        }
    }

    public void SpawnEnemies()
    {
        IEnumerator SpawnEnemiesCoroutine()
        {
            while (true)
            {
                if (shouldSpawn)
                {
                    foreach (var spawnPoint in spawnPoints)
                    {
                        if (!shouldSpawn) break;
                        yield return new WaitForSeconds(spawnRate);
                        SpawnEnemy(spawnPoint);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(spawnRate);
                }
            }
        }

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

    private void DecideSpawnRate()
    {
        if (!dynamicSpawnRate) return;

        var currentTime = Time.timeSinceLevelLoad;
        var enemyCount = GameManager.Instance.enemies.Count;
        float newSpawnRate;
        if (currentTime < 30)
        {
            newSpawnRate = 2f;
        }

        else if (currentTime < 90)
        {
            newSpawnRate = 1f;
        }

        else if (enemyCount < 15)
        {
            newSpawnRate = 0.25f;
        }
        else if (enemyCount < 25)
        {
            newSpawnRate = 0.5f;
        }
        else if (enemyCount < 35)
        {
            newSpawnRate = 1f;
        }
        else
        {
            // TODO: LOSE
            newSpawnRate = 3f;
        }

        spawnRate = newSpawnRate;

        Debug.Log("ENEMY COUNT: " + enemyCount + " SPAWN RATE: " + newSpawnRate);
    }
}