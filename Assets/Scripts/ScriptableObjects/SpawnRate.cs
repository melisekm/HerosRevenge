using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/SpawnRate/New Spawn Rate")]
public class SpawnRate : ScriptableObject
{
    public SpawnTimeInfo[] spawnTimeInfos;
    public SpawnEnemyCountInfo[] spawnEnemyCountInfos;
    public float defaultSpawnRate = 1f;

    public float GetSpawnRate(int enemyCount, float currentTime)
    {
        foreach (SpawnTimeInfo spawnTimeInfo in spawnTimeInfos)
        {
            if (currentTime < spawnTimeInfo.lessThanSeconds)
            {
                return spawnTimeInfo.speed;
            }
        }

        foreach (SpawnEnemyCountInfo spawnEnemyCountInfo in spawnEnemyCountInfos)
        {
            if (enemyCount < spawnEnemyCountInfo.lessThanCount)
            {
                return spawnEnemyCountInfo.speed;
            }
        }

        return defaultSpawnRate;
    }
}

[Serializable]
public struct SpawnTimeInfo
{
    public float lessThanSeconds;
    public float speed;
}

[Serializable]
public struct SpawnEnemyCountInfo
{
    public int lessThanCount;
    public float speed;
}