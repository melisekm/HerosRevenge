using UnityEngine;
using Utils;

public class EffectSpawner : GroundEntitySpawner
{
    protected override void SpawnGroundEntity(ScriptableObject entity)
    {
        if (entity is ScriptableEffect scriptableEffect)
        {
            Effect effect = Instantiate(scriptableEffect.effect, position, Quaternion.identity);
            effect.Initialize(scriptableEffect, Faction.Player);
        }
    }

    protected override ScriptableObject GetEntity()
    {
        return ResourceSystem.Instance.GetRandomEffect();
    }
}