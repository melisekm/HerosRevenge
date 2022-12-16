using UnityEngine;

public class EffectSpawner : GroundEntitySpawner
{
    public Faction defaultFaction = Faction.Player;
    public Vector2 minMaxEffectRadius = new(3f, 3f);

    protected override void SpawnGroundEntity(ScriptableObject entity)
    {
        if (entity is ScriptableEffect scriptableEffect)
        {
            Effect effect = Instantiate(scriptableEffect.effect, position, Quaternion.identity);
            effect.Initialize(scriptableEffect, Random.Range(minMaxEffectRadius.x, minMaxEffectRadius.y),
                defaultFaction);
        }
    }

    protected override ScriptableObject GetEntity()
    {
        return ResourceSystem.Instance.GetRandomEffect();
    }
}