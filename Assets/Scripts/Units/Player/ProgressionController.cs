using System;
using Managers;
using UnityEngine;

namespace Units.Player
{
    public class ProgressionController
    {
        private PlayerStats playerStats;
        private Attributes playerAttributes;
        private float levelUpMultiplier;
        public static event Action<PlayerStats, Attributes, bool, RewardGenerator.Reward[]> OnLevelUp;
        public static event Action<float, float> OnExperienceChanged;
        public static event Action<float> OnGoldChanged;
        private RewardGenerator rewardGenerator;

        public ProgressionController(PlayerUnit playerUnit, float levelUpMultiplier, int rewardsCount)
        {
            playerStats = playerUnit.stats;
            playerAttributes = playerUnit.attributes;
            this.levelUpMultiplier = levelUpMultiplier;
            OnLevelUp?.Invoke(playerStats, playerAttributes, true, null);
            rewardGenerator = new RewardGenerator(playerUnit, rewardsCount);
        }

        public void PickUpEnergy(int amount)
        {
            if (playerAttributes.health.actual <= 0) return;

            playerStats.xp.actual += amount;
            OnExperienceChanged?.Invoke(playerStats.xp.actual, playerStats.xp.max);
            if (playerStats.xp.actual >= playerStats.xp.max && playerStats.level.actual < playerStats.level.max)
            {
                LevelUp();
            }
        }

        public void PickupGold(float amount)
        {
            playerStats.gold.actual += amount;
            OnGoldChanged?.Invoke(playerStats.gold.actual);
        }


        private void LevelUp()
        {
            // TODO add level up effect
            // TODO add level up sound
            playerStats.xp.actual = playerStats.xp.min;
            playerStats.xp.max = Mathf.RoundToInt(playerStats.xp.max * levelUpMultiplier);
            playerStats.level.actual++;
            playerStats.gold.actual += playerStats.gold.increasePerLevel;


            LevelUpAttribute(playerAttributes.health);
            LevelUpAttribute(playerAttributes.speed);
            LevelUpAttribute(playerAttributes.attackPower);
            LevelUpAttribute(playerAttributes.cooldownRecovery);
            LevelUpAttribute(playerAttributes.defenseRating);
            OnLevelUp?.Invoke(playerStats, playerAttributes, false, rewardGenerator.GenerateRewards());
        }

        private void LevelUpAttribute(Attribute attr)
        {
            float newValue = attr.initial + attr.increasePerLevel;
            if (newValue < attr.max)
            {
                attr.initial = newValue;
                attr.actual = attr.initial;
            }
            else
            {
                attr.initial = attr.max;
                attr.actual = attr.max;
            }
        }
    }
}