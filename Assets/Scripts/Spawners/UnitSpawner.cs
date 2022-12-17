using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : Spawner
{
    public EnemyType spawnType;
    public bool spawnRandomEnemyTypes = true;
    public bool dynamicSpawnRate = true;
    public SpawnRate spawnRate;
    private readonly List<Transform> spawnPoints = new();
    private int enemyCount;
    private float enemyPowerMultiplier;


    private void Awake()
    {
        var spawnPointsGo = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (var spawnPoint in spawnPointsGo)
        {
            spawnPoints.Add(spawnPoint.transform);
        }
    }

    private void Start()
    {
        var playerContainerGo = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerGo && playerContainerGo.TryGetComponent(out PlayerContainer playerContainer))
        {
            enemyPowerMultiplier = playerContainer.GetArenaPowerMultiplier();
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

    public static event Action<EnemyUnit> OnEnemySpawned;

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
            var enemyAttributes = new Attributes(enemyScriptable.attributes);
            SetAttributeBasedOnLevel(enemyAttributes.health);
            SetAttributeBasedOnLevel(enemyAttributes.attackPower);
            enemy.energyDropAmount = enemyScriptable.energyDropAmount;
            enemy.energyDropAmount *= (int)enemyPowerMultiplier;
            enemy.SetAttributes(enemyAttributes);
            OnEnemySpawned?.Invoke(enemy);
            DecideSpawnRate();
        }
    }

    private void SetAttributeBasedOnLevel(Attribute attr)
    {
        attr.initial *= (int)enemyPowerMultiplier;
        attr.actual *= (int)enemyPowerMultiplier;
    }


    public override void Activate()
    {
        IEnumerator SpawnEnemiesCoroutine()
        {
            while (true)
            {
                if (isSpawnerActive)
                {
                    foreach (var spawnPoint in spawnPoints)
                    {
                        if (!isSpawnerActive) break;
                        yield return new WaitForSeconds(spawnTimer);
                        SpawnEnemy(spawnPoint);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(spawnTimer);
                }
            }
        }

        isSpawnerActive = true;
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

        spawnTimer = spawnRate.GetSpawnRate(enemyCount, Time.timeSinceLevelLoad);
    }
}