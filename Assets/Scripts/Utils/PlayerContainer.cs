using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [NonSerialized] public Attributes playerAttributes;
    [NonSerialized] public PlayerStats playerStats;
    [NonSerialized] public List<AbilityType> abilityTypes = new();
    [NonSerialized] public AbilityType ultimateType;

    [NonSerialized] public Arena currentArena;

    // dict of arena and completion status
    public List<Arena> arenas;
    private HashSet<Arena> unlockedArenas = new();
    private HashSet<Arena> completedArenas = new();

    public float upgradeCostMultiplier = 1.5f;
    public float upgradeCostLevelThreshold = 10f; // doubles every 10 levels
    
    private void Awake()
    {
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        playerAttributes = new Attributes(playerScriptable.attributes);
        playerStats = new PlayerStats(playerScriptable.playerStats);

        currentArena = arenas.First();
        if (currentArena.mustCompleteArenas.Count == 0)
        {
            unlockedArenas.Add(currentArena);
        }
    }

    private void DecideBuyableUpgradeCostMultiplier()
    {
        if(playerStats.level.actual > upgradeCostLevelThreshold)
        {
            upgradeCostMultiplier *= 1.5f;
            upgradeCostLevelThreshold *= 2f;
        }
    }

    public void BuyUpgrade(Attribute attr)
    {
        playerStats.gold.actual -= attr.upgradeCost;
        attr.BuyUpgrade(upgradeCostMultiplier);
    }
    
    public Arena GetArenaByName(string arenaName)
    {
        return arenas.FirstOrDefault(arena => arena.sceneName == arenaName);
    }

    public void CompleteCurrentArena()
    {
        completedArenas.Add(currentArena);
        foreach (var arena in arenas)
        {
            if (arena == currentArena) continue;
            
            var mustCompleteArenas = arena.mustCompleteArenas;
            if (mustCompleteArenas.FirstOrDefault(x => x.sceneName == currentArena.sceneName))
            {
                unlockedArenas.Add(arena);
            }
        }

        DecideBuyableUpgradeCostMultiplier();
    }

    public bool IsArenaUnlocked(Arena arena)
    {
        // has to be unlocked and not completed
        return unlockedArenas.Contains(arena) && !completedArenas.Contains(arena);
    }


    public bool IsAttributeBuyable(Attribute attr)
    {
        return playerStats.gold.actual >= attr.upgradeCost;
    }
}