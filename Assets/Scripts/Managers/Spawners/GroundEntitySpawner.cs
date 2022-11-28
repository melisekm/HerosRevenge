using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEntitySpawner : MonoBehaviour
{
    public Vector2 spawnArea;

    protected Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
    }
}