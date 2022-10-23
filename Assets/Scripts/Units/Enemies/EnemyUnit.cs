using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyUnit : Unit
{
    public AIDestinationSetter destinationSetter;
    public AIPath aiPath; // A* controls the movement of the enemy
    // it also has stopping distance, speed, slowdown distance..
    private void Start()
    {
        destinationSetter.target = GameObject.FindWithTag("Player").transform;
    }

    public override void SetStats(Stats newStats)
    {
        base.SetStats(newStats);
        aiPath.maxSpeed = Stats.speed;

    }
    private void Update()
    {
        
    }
    
}
