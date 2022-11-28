using System.Linq;
using UnityEngine;

namespace Managers
{
    public class RewardGenerator
    {
        private PlayerUnit playerUnit;
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

        public RewardGenerator(PlayerUnit playerUnit, int rewardsCount)
        {
            this.playerUnit = playerUnit;
            this.rewardsCount = rewardsCount;
        }

        public Reward[] GenerateRewards()
        {
            Reward[] nextRewards = new Reward[rewardsCount];
            // filter those which have minlevel less than playerunit.stats.level.actual
            var abilities = ResourceSystem.Instance.abilities.Where(a => a.minLevel <= playerUnit.stats.level.actual)
                .ToList();
            var statUpgrades = ResourceSystem.Instance.statUpgrades;
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