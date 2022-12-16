using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Units.Player;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUISetter : MonoBehaviour
{
    public GameObject levelUpPanel;
    public TMP_Text levelNumber;
    public List<UIAbilitySlot> abilitySlots;


    public static event Action<ScriptableReward> OnRewardSelected;

    [Serializable]
    public struct UIAbilitySlot
    {
        public TMP_Text name;
        public TMP_Text description;
        public TMP_Text type;
        public Image icon;
        public Button button;
    }

    [Header("Attributes")]
    public TMP_Text damage;
    public TMP_Text speed;
    public TMP_Text defense;
    public TMP_Text cooldown;
    public TMP_Text pickup;
    public TMP_Text health;


    public void SetLevelUpUI(PlayerStats playerStats, Attributes playerAttributes,
        RewardGenerator.Reward[] nextRewards)
    {
        damage.text = playerAttributes.attackPower.actual.ToString("F0");
        speed.text = playerAttributes.speed.actual.ToString("0.#");
        defense.text = playerAttributes.defenseRating.actual.ToString("P0");
        cooldown.text = playerAttributes.cooldownRecovery.actual.ToString("P0");
        pickup.text = playerAttributes.pickupRange.actual.ToString("0.#");
        health.text = playerAttributes.health.actual.ToString("F0");
        levelNumber.text = playerStats.level.actual.ToString("F0");
        levelUpPanel.SetActive(true);


        for (int i = 0; i < abilitySlots.Count; i++)
        {
            ScriptableReward reward = nextRewards[i].scriptableReward;

            abilitySlots[i].name.text = reward.rewardName;
            abilitySlots[i].description.text = reward.description;
            abilitySlots[i].type.text = nextRewards[i].rewardType.ToString();
            abilitySlots[i].icon.sprite = reward.icon;
            abilitySlots[i].button.onClick.RemoveAllListeners();
            abilitySlots[i].button.interactable = false;
            abilitySlots[i].button.onClick.AddListener(() =>
            {
                OnRewardSelected?.Invoke(reward);
                levelUpPanel.SetActive(false);
            });
            StartCoroutine(EnableButton(abilitySlots[i].button));
        }

        IEnumerator EnableButton(Button btn)
        {
            yield return new WaitForSecondsRealtime(1f);
            btn.interactable = true;
        }
    }
}