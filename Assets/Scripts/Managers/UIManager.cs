using Managers;
using TMPro;
using Units.Player;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text healthText;
    public TMP_Text deathText;
    public TMP_Text xpText;
    public TMP_Text goldText;
    public TMP_Text levelText;
    public TMP_Text timeText;
    public TMP_Text selectedAbilityNumber;
    private LevelUpSelectionHandler.Reward[] nextRewards;
    public LevelUpUISetter levelUpUISetter;

    private void OnEnable()
    {
        GameManager.OnUpdateTime += SetTime;
        PlayerControls.OnSwitchAbility += SetAbility;
        ProgressionController.OnLevelUp += OnLevelUp;
        PlayerUnit.OnPlayerTakeDamage += SetHealth;
    }


    private void SetHealth(float newHealth, float maxHealth)
    {
        healthText.text = newHealth.ToString() + "/" + maxHealth.ToString();
    }

    private void OnLevelUp(PlayerStats playerStats, Attributes playerAttributes, bool initial,
        LevelUpSelectionHandler.Reward[] nextRewards)
    {
        levelText.text = playerStats.level.actual.ToString();
        xpText.text = playerStats.xp.actual.ToString() + "/" + playerStats.xp.max.ToString();
        goldText.text = playerStats.gold.actual.ToString();
        healthText.text = playerAttributes.health.actual.ToString() + "/" + playerAttributes.health.initial.ToString();

        if (initial) return;
        this.nextRewards = nextRewards;
        levelUpUISetter.SetLevelUpUI(playerStats, playerAttributes, nextRewards);
    }



    private void SetAbility(int index)
    {
        selectedAbilityNumber.text = (index + 1).ToString();
    }

    private void OnDisable()
    {
        GameManager.OnUpdateTime -= SetTime;
        PlayerControls.OnSwitchAbility -= SetAbility;
        ProgressionController.OnLevelUp -= OnLevelUp;
        PlayerUnit.OnPlayerTakeDamage -= SetHealth;
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