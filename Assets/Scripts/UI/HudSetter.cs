using Managers;
using TMPro;
using Units.Player;
using UnityEngine;

public class HudSetter : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text deathText;
    public TMP_Text xpText;
    public TMP_Text goldText;
    public TMP_Text levelText;
    public TMP_Text timeText;
    public TMP_Text selectedAbilityNumber;
    public LevelUpUISetter levelUpUISetter;

    private void OnEnable()
    {
        GameManager.OnUpdateTime += SetTime;
        PlayerControls.OnSwitchAbility += SetAbility;
        ProgressionController.OnLevelUp += OnLevelUp;
        ProgressionController.OnExperienceChanged += SetExperience;
        ProgressionController.OnGoldChanged += SetGold;
        PlayerUnit.OnPlayerHealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateTime -= SetTime;
        PlayerControls.OnSwitchAbility -= SetAbility;
        ProgressionController.OnLevelUp -= OnLevelUp;
        PlayerUnit.OnPlayerHealthChanged -= SetHealth;
        ProgressionController.OnExperienceChanged -= SetExperience;
        ProgressionController.OnGoldChanged -= SetGold;
    }

    private void SetExperience(float actual, float max)
    {
        xpText.text = actual + "/" + max;
    }

    private void SetGold(float actual)
    {
        goldText.text = actual.ToString();
    }

    private void SetHealth(float newHealth, float maxHealth)
    {
        healthText.text = newHealth + "/" + maxHealth;
        if(newHealth <= 0)
        {
            deathText.gameObject.SetActive(true);
        }
    }

    private void OnLevelUp(PlayerStats playerStats, Attributes playerAttributes, bool initial,
        RewardGenerator.Reward[] nextRewards)
    {
        levelText.text = playerStats.level.actual.ToString();
        xpText.text = playerStats.xp.actual + "/" + playerStats.xp.max;
        goldText.text = playerStats.gold.actual.ToString();
        healthText.text = playerAttributes.health.actual + "/" + playerAttributes.health.initial;

        if (initial) return;
        levelUpUISetter.SetLevelUpUI(playerStats, playerAttributes, nextRewards);
    }


    private void SetAbility(int index)
    {
        selectedAbilityNumber.text = (index + 1).ToString();
    }


    // https://github.com/michalferko/tvorbahier/blob/master/PacMan/Assets/Scripts/UIManager.cs
    void SetTime(float currentTime)
    {
        // transform time to desired format
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string niceTime = $"{minutes:00}:{seconds:00}";

        // Set UI time text
        timeText.text = niceTime;
    }
}