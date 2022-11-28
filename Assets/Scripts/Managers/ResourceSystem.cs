using System.Collections.Generic;
using System.Linq;
using Units.Player;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    public List<ScriptableEnemy> enemies;
    public List<ScriptableAbility> abilities;
    public List<ScriptableEffect> effects;
    public List<ScriptableStatUpgrade> statUpgrades;
    public List<ScriptablePowerUp> powerUps;
    public ScriptablePlayer player;

    protected override void Awake()
    {
        base.Awake();
        enemies = Resources.LoadAll<ScriptableEnemy>("Units/Enemies").ToList();
        abilities = Resources.LoadAll<ScriptableAbility>("Abilities").ToList();
        statUpgrades = Resources.LoadAll<ScriptableStatUpgrade>("StatUpgrades").ToList();
        effects = Resources.LoadAll<ScriptableEffect>("Effects").ToList();
        player = Resources.Load<ScriptablePlayer>("Units/Player");
        powerUps = Resources.LoadAll<ScriptablePowerUp>("PowerUps").ToList();
    }
    
    public ScriptablePowerUp GetPowerUpByType(PowerUpType type)
    {
        return powerUps.Find(x => x.powerUpType == type);
    }

    public ScriptableEffect GetRandomDamageEffect()
    {
        return effects[Random.Range(0, effects.Count)];
    }


    public ScriptableEffect GetRandomEffect()
    {
        return GetRandomDamageEffect();
    }

    public ScriptableEffect GetEffectByType(EffectType type)
    {
        return effects.Find(x => x.effectType == type);
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