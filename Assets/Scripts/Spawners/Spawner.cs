using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public bool isSpawnerActive;
    public float spawnTimer = 5f;

    public virtual void Activate()
    {
        isSpawnerActive = true;
    }
}