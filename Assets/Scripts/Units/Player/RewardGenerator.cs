using System.Collections.Generic;
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
            public ScriptableReward scriptableReward;
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
                var randomRoll = Random.Range(0, 100);
                RewardType nextRewardType = randomRoll < 75 ? RewardType.Ability : RewardType.Stat;
                // make sure there is at least one ability or stat upgrade regardless of what rolls
                if ((abilities.Count > 0 && nextRewardType == RewardType.Ability) || statUpgrades.Count == 0)
                {
                    nextRewards[i] = new Reward
                    {
                        rewardType = RewardType.Ability,
                        scriptableReward = abilities[Random.Range(0, abilities.Count)]
                    };
                }

                else
                {
                    nextRewards[i] = new Reward
                    {
                        rewardType = RewardType.Stat,
                        scriptableReward = statUpgrades[Random.Range(0, statUpgrades.Count)]
                    };
                }
            }

            return nextRewards;
        }
    }
}