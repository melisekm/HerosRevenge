using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyUnit : Unit
{
    public AIDestinationSetter destinationSetter;
    // A* controls the movement of the enemy, it also has stopping distance, speed, slowdown distance..
    public AIPath aiPath; 
    protected override void Awake()
    {
        base.Awake();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        aiPath = gameObject.GetComponent<AIPath>();
        destinationSetter.target = GameObject.FindWithTag("Player").transform;
    }

    public override void SetStats(Stats newStats)
    {
        base.SetStats(newStats);
        aiPath.maxSpeed = Stats.speed;
    }

}