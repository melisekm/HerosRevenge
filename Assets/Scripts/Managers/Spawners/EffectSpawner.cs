using UnityEngine;
using Utils;

public class EffectSpawner : GroundEntitySpawner
{
    public float spawnEffectEverySeconds = 5f;
    protected float effectTimer;

    protected virtual void Update()
    {
        if (effectTimer <= 0)
        {
            
            SpawnEffect(GetEffect());
            effectTimer = spawnEffectEverySeconds;
        }
        else
        {
            effectTimer -= Time.deltaTime;
        }
    }

    protected virtual ScriptableEffect GetEffect()
    {
        return ResourceSystem.Instance.GetRandomEffect();
    }

    private void SpawnEffect(ScriptableEffect scriptableEffect)
    {
        // get random position in a rectangle
        Vector3 position = GetRandomPosition();
        // spawn effect
        Effect effect = Instantiate(scriptableEffect.effect, position, Quaternion.identity);
        effect.Initialize(scriptableEffect, Faction.Player);
    }
}