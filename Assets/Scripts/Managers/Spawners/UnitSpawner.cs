using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private readonly List<Transform> spawnPoints = new();
    public float spawnRate = 1f;
    public bool shouldSpawn = true;
    public EnemyType spawnType;
    public bool spawnRandomEnemyTypes = true;
    public bool dynamicSpawnRate = true;

    public static event Action<EnemyUnit> OnEnemySpawned;


    private void Awake()
    {
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
    }
}