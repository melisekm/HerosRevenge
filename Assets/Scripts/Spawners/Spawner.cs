using System;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [NonSerialized] protected bool isSpawnerActive;
    public float spawnTimer = 5f;

    public virtual void Activate()
    {
        isSpawnerActive = true;
    }
}