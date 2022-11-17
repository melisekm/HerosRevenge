using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public float spawnRate = 5f;
    private float timer;
    public bool isActive = true;
    

    private void Update()
    {
        if (!isActive) return;
        
        if (timer <= 0)
        {
            timer = spawnRate;
            ScriptableEffect scriptableEffect = ResourceSystem.Instance.GetRandomEffect();
            // get random position in a rectangle
            Vector3 position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            // spawn effect
            if (scriptableEffect.effectType == EffectType.Damage)
            {
                Effect effect = Instantiate(scriptableEffect.effect, position, Quaternion.identity);
                effect.Initialize(scriptableEffect);
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
