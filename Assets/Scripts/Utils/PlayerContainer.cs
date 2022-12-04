using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [NonSerialized] public Attributes playerAttributes;
    [NonSerialized] public PlayerStats playerStats;
    [NonSerialized] public List<AbilityType> abilityTypes = new();
    [NonSerialized] public AbilityType ultimateType;

    [NonSerialized] public string currentArena;

    // id of level and if it is unlocked
    public Dictionary<string, bool> levelUnlocked;

    private void Awake()
    {
        // create new instance of levelUnlocked with 2 levels unlocked in constructor
        // or load from file in future
        levelUnlocked = new Dictionary<string, bool>
        {
            { "Arena_Entrance", true },
            { "Arena_Name", false }
        };
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        playerAttributes = new Attributes(playerScriptable.attributes);
        playerStats = new PlayerStats(playerScriptable.playerStats);
    }

    public void Save(Attributes playerAttributes, PlayerStats playerStats, string currentArena,
        Dictionary<string, bool> levelUnlocked, List<AbilityType> abilityTypes, AbilityType ultimateType)
    {
        this.playerAttributes = playerAttributes;
        this.playerStats = playerStats;
        this.currentArena = currentArena;
        this.levelUnlocked = levelUnlocked;
        this.abilityTypes = abilityTypes;
        this.ultimateType = ultimateType;
    }
}