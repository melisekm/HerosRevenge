using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [NonSerialized] public Attributes playerAttributes;
    [NonSerialized] public PlayerStats playerStats;
    [NonSerialized] public List<AbilityType> abilityTypes = new();

    [NonSerialized] public int currentArena;

    // id of level and if it is unlocked
    public Dictionary<int, bool> levelUnlocked;

    private void Awake()
    {
        // create new instance of levelUnlocked with 2 levels unlocked in constructor
        // or load from file in future
        levelUnlocked = new Dictionary<int, bool>
        {
            { 1, true },
            { 2, false }
        };
        currentArena = 1;
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        playerAttributes = new Attributes(playerScriptable.attributes);
        playerStats = new PlayerStats(playerScriptable.playerStats);
    }

    public void Save(Attributes playerAttributes, PlayerStats playerStats, int currentArena,
        Dictionary<int, bool> levelUnlocked, List<AbilityType> abilityTypes)
    {
        this.playerAttributes = playerAttributes;
        this.playerStats = playerStats;
        this.currentArena = currentArena;
        this.levelUnlocked = levelUnlocked;
        this.abilityTypes = abilityTypes;
    }
}