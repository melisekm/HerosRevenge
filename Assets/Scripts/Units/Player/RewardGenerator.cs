using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units.Player
{
    public class RewardGenerator
    {
        private int rewardsCount;

        public enum RewardType
        {
            Ability,
            Stat
        }

        public class Reward
        {
            public RewardType rewardType;
            public ScriptableReward reward;
        }

        public RewardGenerator(int rewardsCount)
        {
            this.rewardsCount = rewardsCount;
        }

        public Reward[] GenerateRewards(List<ScriptableAbility> abilities, List<ScriptableStatUpgrade> statUpgrades)
        {
            Reward[] nextRewards = new Reward[rewardsCount];
            // filter those which have minlevel less than playerunit.stats.level.actual
            for (int i = 0; i < nextRewards.Length; i++)
            {
                var nextRewardType = (RewardType)Random.Range(0, nextRewards.Length);
                if (nextRewardType == RewardType.Ability)
                {
                    nextRewards[i] = new Reward
                    {
                        rewardType = RewardType.Ability,
                        reward = abilities[Random.Range(0, abilities.Count)]
                    };
                }

                else if (nextRewardType == RewardType.Stat)
                {
                    nextRewards[i] = new Reward
                    {
                        rewardType = RewardType.Stat,
                        reward = statUpgrades[Random.Range(0, statUpgrades.Count)]
                    };
                }
            }
            return nextRewards;
        }
    }
}