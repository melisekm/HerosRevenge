using UnityEngine;

public abstract class GroundEntitySpawner : Spawner
{
    public Vector2 spawnAreaXminXmax;
    public Vector2 spawnAreaYminYmax;
    protected Vector3 position;
    protected float timeUntilSpawn;

    protected virtual void Update()
    {
        if (!isSpawnerActive) return;

        if (timeUntilSpawn <= 0)
        {
            position = GetRandomPosition();
            SpawnGroundEntity(GetEntity());
            timeUntilSpawn = spawnTimer;
        }
        else
        {
            timeUntilSpawn -= Time.deltaTime;
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(spawnAreaXminXmax.x, spawnAreaXminXmax.y),
            Random.Range(spawnAreaYminYmax.x, spawnAreaYminYmax.y), 0);
    }

    protected abstract void SpawnGroundEntity(ScriptableObject entity);
    protected abstract ScriptableObject GetEntity();
}