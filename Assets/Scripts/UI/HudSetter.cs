using TMPro;
using Units.Player;
using UnityEngine;

public class HudSetter : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text deathText;
    public TMP_Text winText;
    public TMP_Text xpText;
    public TMP_Text goldText;
    public TMP_Text levelText;
    public TMP_Text timeText;
    public TMP_Text selectedAbilityNumber;
    public TMP_Text winConditionText;
    public LevelUpUISetter levelUpUISetter;
    public GameObject powerUpPanel;
    public GameObject powerUpBar;

    public WinConditionChecker winConditionChecker;
    public PlayerUnit playerUnit;

    private void OnEnable()
    {
        GameManager.OnUpdateTime += SetTime;
        PlayerControls.OnSwitchAbility += SetAbility;
        ProgressionController.OnLevelUp += OnLevelUp;
        PlayerUnit.OnPlayerDied += ShowYouDiedText;
        StatPowerup.OnStatUpgradeActivated += OnStatUpgradeActivated;
        StatPowerup.OnStatUpgradeDeactivated += OnStatUpgradeDeactivated;
        WinConditionChecker.OnWinConditionMet += ShowWinText;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateTime -= SetTime;
        PlayerControls.OnSwitchAbility -= SetAbility;
        playerUnit.attributes.health.OnValueChanged -= SetHealth;
        playerUnit.stats.xp.OnValueChanged -= SetExperience;
        playerUnit.stats.gold.OnValueChanged -= SetGold;
        ProgressionController.OnLevelUp -= OnLevelUp;
        PlayerUnit.OnPlayerDied -= ShowYouDiedText;
        StatPowerup.OnStatUpgradeActivated -= OnStatUpgradeActivated;
        StatPowerup.OnStatUpgradeDeactivated -= OnStatUpgradeDeactivated;
        WinConditionChecker.OnWinConditionMet -= ShowWinText;
    }

    private void Start()
    {
        playerUnit.attributes.health.OnValueChanged += SetHealth;
        playerUnit.stats.xp.OnValueChanged += SetExperience;
        playerUnit.stats.gold.OnValueChanged += SetGold;
        winConditionText.text = winConditionChecker.description;
    }

    private void OnStatUpgradeActivated(ScriptableStatUpgrade obj)
    {
        var powerupbarGo = Instantiate(powerUpBar, powerUpPanel.transform);
        // get text child in powerupbar
        var powerupbarText = powerupbarGo.GetComponentInChildren<TMP_Text>();
        var amount = obj.statType != StatType.Speed && obj.amount < 1
            ? (obj.amount * 100).ToString("F2") + "%"
            : obj.amount.ToString();
        powerupbarText.text = $"{obj.rewardName}\n+{amount}";
    }

    private void OnStatUpgradeDeactivated(ScriptableStatUpgrade _)
    {
        if (powerUpPanel && powerUpPanel.transform.childCount > 0)
        {
            var powerupbarGo = powerUpPanel.transform.GetChild(0)?.gameObject;
            if (powerupbarGo)
            {
                Destroy(powerupbarGo);
            }
        }
    }

    private void ShowYouDiedText()
    {
        if (deathText)
        {
            deathText.gameObject.SetActive(true);
        }
    }
    
    private void ShowWinText()
    {
        if (winText)
        {
            winText.gameObject.SetActive(true);
        }
    }

    private void SetExperience(Attribute xp)
    {
        xpText.text = xp.actual + "/" + xp.max;
    }

    private void SetGold(Attribute gold)
    {
        goldText.text = gold.actual.ToString("F0");
    }

    private void SetHealth(Attribute health)
    {
        healthText.text = health.actual.ToString("F0") + "/" + health.initial;
    }

    private void OnLevelUp(PlayerStats playerStats, Attributes playerAttributes, bool initial,
        RewardGenerator.Reward[] nextRewards)
    {
        levelText.text = playerStats.level.actual.ToString("F0");
        xpText.text = playerStats.xp.actual + "/" + playerStats.xp.max;
        goldText.text = playerStats.gold.actual.ToString("F0");
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