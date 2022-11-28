using System.Collections.Generic;

public class PlayerContainer
{
    public Attributes playerAttributes;
    public PlayerStats playerStats;
    public int currentLevel;
    // id of level and if it is unlocked
    public Dictionary<int, bool> levelUnlocked;

    public PlayerContainer()
    {
        // create new instance of levelUnlocked with 2 levels unlocked in constructor
        // or load from file in future
        levelUnlocked = new Dictionary<int, bool>
        {
            { 1, true },
            { 2, false }
        };
        currentLevel = 1;
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        playerAttributes = new Attributes(playerScriptable.attributes);
        playerStats = new PlayerStats(playerScriptable.playerStats);
    }

    public PlayerContainer(Attributes playerAttributes, PlayerStats playerStats, int currentLevel,
        Dictionary<int, bool> levelUnlocked)
    {
        Save(playerAttributes, playerStats, currentLevel, levelUnlocked);
    }

    public void Save(Attributes playerAttributes, PlayerStats playerStats, int currentLevel,
        Dictionary<int, bool> levelUnlocked)
    {
        this.playerAttributes = playerAttributes;
        this.playerStats = playerStats;
        this.currentLevel = currentLevel;
        this.levelUnlocked = levelUnlocked;
    }
}