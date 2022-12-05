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
    private HashSet<Arena> completedArenas = new();
    

    public Arena GetArenaByName(string arenaName)
    {
        return arenas.FirstOrDefault(arena => arena.sceneName == arenaName);
    }

    public void CompleteCurrentArena()
    {
        foreach (var arena in arenas)
        {
            if (arena == currentArena) continue;
            
            var mustCompleteArenas = arena.mustCompleteArenas;
            if (mustCompleteArenas.FirstOrDefault(x => x.sceneName == currentArena.sceneName))
            {
                completedArenas.Add(arena);
            }
        }

        // to prevent repetition of the same arena
        // completedArenas.Remove(currentArena);
    }

    public bool IsArenaUnlocked(Arena arena)
    {
        // if arena is not in dict, return false
        return completedArenas.Contains(arena);
    }

    private void Awake()
    {
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        playerAttributes = new Attributes(playerScriptable.attributes);
        playerStats = new PlayerStats(playerScriptable.playerStats);

        currentArena = arenas.First();
        if (currentArena.mustCompleteArenas.Count == 0)
        {
            completedArenas.Add(currentArena);
        }
    }
}