using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public float spawnRate = 5f;
    private float timer;
    public bool isActive = true;
    public Vector2 spawnArea;
    

    private void Update()
    {
        if (!isActive) return;
        
        if (timer <= 0)
        {
            SpawnEffect();
            timer = spawnRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void SpawnEffect()
    {
        ScriptableEffect scriptableEffect = ResourceSystem.Instance.GetRandomEffect();
        // get random position in a rectangle
        Vector3 position = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
        // spawn effect
        if (scriptableEffect.effectType == EffectType.Damage)
        {
            Effect effect = Instantiate(scriptableEffect.effect, position, Quaternion.identity);
            effect.Initialize(scriptableEffect);
        }
    }
}
