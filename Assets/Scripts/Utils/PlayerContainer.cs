using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    // dict of arena and completion status
    public List<Arena> arenas;

    public float upgradeCostMultiplier = 1.5f;
    public float upgradeCostLevelThreshold = 10f; // doubles every 10 levels
    public int newGamePlus;
    public int killCount;
    public int deathsCount;
    [NonSerialized] public List<AbilityType> abilityTypes = new();
    private HashSet<Arena> completedArenas = new();

    [NonSerialized] public Arena currentArena;
    [NonSerialized] public Attributes playerAttributes;
    [NonSerialized] public PlayerStats playerStats;
    [NonSerialized] public AbilityType ultimateType;
    private HashSet<Arena> unlockedArenas = new();

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
        if (playerStats.level.actual > upgradeCostLevelThreshold)
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

    public void CompleteCurrentArena(int killCount)
    {
        this.killCount += killCount;
        completedArenas.Add(currentArena);
        foreach (var arena in arenas.FindAll(x => !completedArenas.Contains(x)))
        {
            if (arena.mustCompleteArenas.All(x => completedArenas.Contains(x)))
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

    public float GetArenaPowerMultiplier()
    {
        // if enemypower is 1 and ngplus is 1, and there are 3 arenas, then the multiplier is 1+1*3=4, or 2+1*3=5... 
        return currentArena.enemyPowerMultiplier + newGamePlus * arenas.Count;
    }

    public void BeginNewGamePlus()
    {
        newGamePlus++;
        completedArenas.Clear();
    }

    public void FailCurrentArena(int killCount)
    {
        this.killCount += killCount;
        deathsCount++;
    }
}