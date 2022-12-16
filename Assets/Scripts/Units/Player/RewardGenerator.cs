using System.Collections.Generic;
using UnityEngine;

namespace Units.Player
{
    public class RewardGenerator
    {
        public enum RewardType
        {
            Ability,
            Stat
        }

        private int rewardsCount;

        public RewardGenerator(int rewardsCount)
        {
            this.rewardsCount = rewardsCount;
        }

        public Reward[] GenerateRewards(List<ScriptableAbility> abilitiesOrig,
            List<ScriptableStatUpgrade> statUpgradesOrig)
        {
            // make local copy of lists
            List<ScriptableAbility> abilities = new List<ScriptableAbility>(abilitiesOrig);
            List<ScriptableStatUpgrade> statUpgrades = new List<ScriptableStatUpgrade>(statUpgradesOrig);

            Reward[] nextRewards = new Reward[rewardsCount];
            // filter those which have minlevel less than playerunit.stats.level.actual
            for (int i = 0; i < nextRewards.Length; i++)
            {
                var randomRoll = Random.Range(0, 100);
                RewardType nextRewardType = randomRoll < 75 ? RewardType.Ability : RewardType.Stat;
                // make sure there is at least one ability or stat upgrade regardless of what rolls
                if ((abilities.Count > 0 && nextRewardType == RewardType.Ability) || statUpgrades.Count == 0)
                {
                    var selectedAbility = abilities[Random.Range(0, abilities.Count)];
                    // remove the ability from the list so it can't be selected again
                    abilities.Remove(selectedAbility);
                    nextRewards[i] = new Reward
                    {
                        rewardType = RewardType.Ability,
                        scriptableReward = selectedAbility
                    };
                }

                else
                {
                    var selectedStatUpgrade = statUpgrades[Random.Range(0, statUpgrades.Count)];
                    // remove the stat upgrade from the list so it can't be selected again
                    statUpgrades.Remove(selectedStatUpgrade);
                    nextRewards[i] = new Reward
                    {
                        rewardType = RewardType.Stat,
                        scriptableReward = selectedStatUpgrade
                    };
                }
            }

            return nextRewards;
        }

        public class Reward
        {
            public RewardType rewardType;
            public ScriptableReward scriptableReward;
        }
    }
}