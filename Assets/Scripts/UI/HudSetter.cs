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
    public TMP_Text winConditionText;
    public TMP_Text killsTextValue;
    public LevelUpUISetter levelUpUISetter;
    public GameObject powerUpPanel;
    public GameObject powerUpBar;

    public WinConditionChecker winConditionChecker;
    public PlayerUnit playerUnit;

    private void Start()
    {
        playerUnit.attributes.health.OnValueChanged += SetHealth;
        playerUnit.stats.xp.OnValueChanged += SetExperience;
        playerUnit.stats.gold.OnValueChanged += SetGold;
        winConditionText.text = winConditionChecker.description;
    }

    private void OnEnable()
    {
        GameManager.OnUpdateTime += SetTime;
        ProgressionController.OnLevelUp += OnLevelUp;
        PlayerUnit.OnPlayerDied += ShowYouDiedText;
        StatPowerup.OnStatUpgradeActivated += OnStatUpgradeActivated;
        StatPowerup.OnStatUpgradeDeactivated += OnStatUpgradeDeactivated;
        WinConditionChecker.OnWinConditionMet += ShowWinText;
        GameManager.OnKillCountChange += SetKillCount;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateTime -= SetTime;
        playerUnit.attributes.health.OnValueChanged -= SetHealth;
        playerUnit.stats.xp.OnValueChanged -= SetExperience;
        playerUnit.stats.gold.OnValueChanged -= SetGold;
        ProgressionController.OnLevelUp -= OnLevelUp;
        PlayerUnit.OnPlayerDied -= ShowYouDiedText;
        StatPowerup.OnStatUpgradeActivated -= OnStatUpgradeActivated;
        StatPowerup.OnStatUpgradeDeactivated -= OnStatUpgradeDeactivated;
        WinConditionChecker.OnWinConditionMet -= ShowWinText;
        GameManager.OnKillCountChange -= SetKillCount;
    }

    private void SetKillCount(int num)
    {
        killsTextValue.text = num.ToString();
    }

    private void OnStatUpgradeActivated(ScriptableStatUpgrade upgrade)
    {
        var powerupbarGo = Instantiate(powerUpBar, powerUpPanel.transform);
        // get text child in powerupbar
        var powerupbarText = powerupbarGo.GetComponentInChildren<TMP_Text>();
        var amount = upgrade.statType is StatType.Speed or StatType.PickupRange ||
                     upgrade.amount > 1
            ? upgrade.amount.ToString()
            : upgrade.amount.ToString("P0");
        powerupbarText.text = $"{upgrade.rewardName}\n+{amount}";
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

    private void SetTime(float currentTime)
    {
        timeText.text = FormatTime(currentTime);
    }

    // https://github.com/michalferko/tvorbahier/blob/master/PacMan/Assets/Scripts/UIManager.cs
    public static string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        return $"{minutes:00}:{seconds:00}";
    }
}