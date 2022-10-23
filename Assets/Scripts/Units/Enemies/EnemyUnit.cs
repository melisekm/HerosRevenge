using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyUnit : Unit
{
    public AIDestinationSetter destinationSetter;
    public AIPath aiPath;
    private void Start()
    {
        destinationSetter.target = GameObject.FindWithTag("Player").transform;
    }

    public void SetStats(Stats newStats)
    {
        stats = newStats;
        aiPath.maxSpeed = stats.speed;

    }
    private void Update()
    {
        
    }
    
}
