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

    public PlayerUnit playerUnit;
    private void Start()
    {
        playerUnit.attributes.health.OnValueChanged += SetHealth;
        playerUnit.stats.xp.OnValueChanged += SetExperience;
        playerUnit.stats.gold.OnValueChanged += SetGold;

    }

    private void OnEnable()
    {
        GameManager.OnUpdateTime += SetTime;
        PlayerControls.OnSwitchAbility += SetAbility;
        ProgressionController.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateTime -= SetTime;
        PlayerControls.OnSwitchAbility -= SetAbility;
        playerUnit.attributes.health.OnValueChanged -= SetHealth;
        playerUnit.stats.xp.OnValueChanged -= SetExperience;
        playerUnit.stats.gold.OnValueChanged -= SetGold;
        ProgressionController.OnLevelUp -= OnLevelUp;
    }

    private void SetExperience(Attribute xp)
    {
        xpText.text = xp.actual + "/" + xp.max;
    }

    private void SetGold(Attribute gold)
    {
        goldText.text = gold.actual.ToString();
    }

    private void SetHealth(Attribute health)
    {
        healthText.text = health.actual + "/" + health.initial;
        if(health.actual <= 0)
        {
            if (deathText)
            {
                deathText.gameObject.SetActive(true);
            }
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