using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    public List<ScriptableEnemy> enemies;
    public List<ScriptableAbility> abilities;
    public List<ScriptableEffect> damageEffects;
    public List<ScriptableEffect> collectibleEffects; // TODO: change name
    public ScriptablePlayer player;

    protected override void Awake()
    {
        base.Awake();
        enemies = Resources.LoadAll<ScriptableEnemy>("Units/Enemies").ToList();
        abilities = Resources.LoadAll<ScriptableAbility>("Abilities").ToList();
        var effects = Resources.LoadAll<ScriptableEffect>("Effects").ToList();
        foreach (var effect in effects)
        {
            if (effect.effectType == EffectType.Damage)
                damageEffects.Add(effect);
            else
                collectibleEffects.Add(effect);
        }

        player = Resources.Load<ScriptablePlayer>("Units/Player");
    }

    public ScriptableEffect GetRandomDamageEffect()
    {
        return damageEffects[Random.Range(0, damageEffects.Count)];
    }

    public ScriptableEffect GetRandomCollectibleEffect()
    {
        return collectibleEffects[Random.Range(0, collectibleEffects.Count)];
    }

    public ScriptableEffect GetRandomEffect()
    {
        // if (Random.Range(0, 2) == 0)
        return GetRandomDamageEffect();
        // return GetRandomCollectibleEffect();
    }

    public ScriptableEnemy GetEnemyByType(EnemyType type)
    {
        return enemies.Find(x => x.enemyType == type);
    }

    public ScriptableAbility GetAbilityByType(AbilityType type)
    {
        return abilities.Find(x => x.abilityType == type);
    }

    public ScriptableEnemy GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }
}