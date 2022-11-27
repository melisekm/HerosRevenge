using System.Collections.Generic;

namespace Managers
{
    public class PlayerContainer
    {
        private Attributes playerAttributes;
        private PlayerStats playerStats;
        private int currentLevel;
        private Dictionary<int, bool> levelUnlocked;

        public PlayerContainer()
        {
        }

        public PlayerContainer(Attributes playerAttributes, PlayerStats playerStats, int currentLevel,
            Dictionary<int, bool> levelUnlocked)
        {
            this.playerAttributes = playerAttributes;
            this.playerStats = playerStats;
            this.currentLevel = currentLevel;
            this.levelUnlocked = levelUnlocked;
        }
    }
}