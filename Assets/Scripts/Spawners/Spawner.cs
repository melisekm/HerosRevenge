using System;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public float spawnTimer = 5f;
    [NonSerialized] protected bool isSpawnerActive;

    public virtual void Activate()
    {
        isSpawnerActive = true;
    }
}