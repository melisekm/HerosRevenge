using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
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
    

    public void SetLevelUpUI(PlayerStats playerStats, Attributes playerAttributes, LevelUpSelectionHandler.Reward[] nextRewards)
    {
        levelUpPanel.SetActive(true);
        levelNumber.text = playerStats.level.actual.ToString();
        for (int i = 0; i < abilitySlots.Count; i++)
        {

            ScriptableReward reward = nextRewards[i].reward;
            
            abilitySlots[i].name.text = reward.rewardName;
            abilitySlots[i].description.text = reward.description;
            abilitySlots[i].type.text = nextRewards[i].rewardType.ToString();
            abilitySlots[i].icon.sprite = reward.icon;
            abilitySlots[i].button.onClick.RemoveAllListeners();
            abilitySlots[i].button.onClick.AddListener(() =>
            {
                OnRewardSelected?.Invoke(reward);
                levelUpPanel.SetActive(false);
            });

        }

    }
}
