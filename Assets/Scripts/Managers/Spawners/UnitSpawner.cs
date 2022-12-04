using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private readonly List<Transform> spawnPoints = new();
    public float currentSpawnRate = 1f;
    public bool shouldSpawn = true;
    public EnemyType spawnType;
    public bool spawnRandomEnemyTypes = true;
    public bool dynamicSpawnRate = true;
    private int enemyCount;
    public SpawnRate spawnRate;

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
        enemyCount--;
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
            enemyCount++;
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
                        yield return new WaitForSeconds(currentSpawnRate);
                        SpawnEnemy(spawnPoint);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(currentSpawnRate);
                }
            }
        }

        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private void DecideSpawnRate()
    {
        if (!dynamicSpawnRate) return;
        if (!spawnRate)
        {
            Debug.LogError("Trying to decide spawnrate but spawnrate object not found.");
            return;
        }

        currentSpawnRate = spawnRate.GetSpawnRate(enemyCount, Time.timeSinceLevelLoad);
        Debug.Log($"Spawn rate is now {currentSpawnRate}");
    }
}