using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    public List<ScriptableEnemy> enemies;
    public ScriptablePlayer player;

    protected override void Awake()
    {
        base.Awake();
        enemies = Resources.LoadAll<ScriptableEnemy>("Enemies").ToList();
        player = Resources.Load<ScriptablePlayer>("Player");
    }

    public ScriptableEnemy GetEnemyByType(EnemyType type)
    {
        return enemies.Find(x => x.enemyType == type);
    }

    public ScriptableEnemy GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }
}