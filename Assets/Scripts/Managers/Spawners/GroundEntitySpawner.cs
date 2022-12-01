using UnityEngine;

public abstract class GroundEntitySpawner : MonoBehaviour
{
    public Vector2 spawnArea;

    protected Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
    }
    public float spawnTimer = 5f;
    protected float timeUntilSpawn;
    protected Vector3 position;

    protected virtual void Update()
    {
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