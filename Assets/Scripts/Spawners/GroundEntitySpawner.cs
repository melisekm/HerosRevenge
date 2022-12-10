using UnityEngine;

public abstract class GroundEntitySpawner : Spawner
{
    protected float timeUntilSpawn;
    protected Vector3 position;
    
    public Vector2 spawnAreaXminXmax;
    public Vector2 spawnAreaYminYmax;
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(spawnAreaXminXmax.x, spawnAreaXminXmax.y),
            Random.Range(spawnAreaYminYmax.x, spawnAreaYminYmax.y), 0);
    }

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

    protected abstract void SpawnGroundEntity(ScriptableObject entity);
    protected abstract ScriptableObject GetEntity();
}