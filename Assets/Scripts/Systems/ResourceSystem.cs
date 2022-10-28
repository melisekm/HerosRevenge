using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    public List<ScriptableEnemy> enemies;
    public List<ScriptableAbility> abilities;
    public ScriptablePlayer player;

    protected override void Awake()
    {
        base.Awake();
        enemies = Resources.LoadAll<ScriptableEnemy>("Units/Enemies").ToList();
        abilities = Resources.LoadAll<ScriptableAbility>("Abilities").ToList();
        player = Resources.Load<ScriptablePlayer>("Units/Player");
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