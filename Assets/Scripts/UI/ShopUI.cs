using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private PlayerContainer playerContainer;
    private Attributes playerAttributes;
    private PlayerStats playerStats;
    private List<Attribute> upgradableAttributes;
    private Dictionary<Button, Attribute> buttonToAttribute = new();

    [Header("Current Player Stats")] public TMP_Text playerHealth;
    public TMP_Text playerAttackPower;
    public TMP_Text playerDefenseRating;
    public TMP_Text playerSpeed;
    public TMP_Text playerCooldownRecovery;
    public TMP_Text playerGold;

    [Header("Upgrades Button Texts")] public TextMeshProUGUI healthUpgradeButton;
    public TextMeshProUGUI attackPowerUpgradeButton;
    public TextMeshProUGUI defenseRatingUpgradeButton;
    public TextMeshProUGUI pickupRangeUpgradeButton;
    public TextMeshProUGUI cooldownRecoveryUpgradeButton;
    public List<Button> buttons;


    [Header("Upgrades Costs")] public TMP_Text healthUpgradeCost;
    public TMP_Text attackPowerUpgradeCost;
    public TMP_Text defenseRatingUpgradeCost;
    public TMP_Text pickupRangeUpgradeCost;
    public TMP_Text cooldownRecoveryUpgradeCost;

    private void Start()
    {
        var playerContainerObj = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerObj && playerContainerObj.TryGetComponent(out PlayerContainer playerContainer))
        {
            this.playerContainer = playerContainer;
            playerAttributes = this.playerContainer.playerAttributes;
            playerStats = this.playerContainer.playerStats;
        }
        else
        {
            Debug.LogWarning("PlayerContainer not found");
        }

        upgradableAttributes = new List<Attribute>
        {
            playerAttributes.health,
            playerAttributes.attackPower,
            playerAttributes.defenseRating,
            playerAttributes.pickupRange,
            playerAttributes.cooldownRecovery
        };
        buttonToAttribute = upgradableAttributes
            .Zip(buttons, (attribute, button) => new { attribute, button })
            .ToDictionary(x => x.button, x => x.attribute);
        SetUp();
    }

    public void BuyUpgrade(int index)
    {
        playerContainer.BuyUpgrade(upgradableAttributes[index]);
        SetUp(); // :) 
    }

    private void SetUp()
    {
        SetCurrentStats();
        SetUpgradeStats();
        SetUpgradeCosts();
    }

    private void SetUpgradeCosts()
    {
        healthUpgradeCost.text = playerAttributes.health.upgradeCost.ToString();
        attackPowerUpgradeCost.text = playerAttributes.attackPower.upgradeCost.ToString();
        defenseRatingUpgradeCost.text = playerAttributes.defenseRating.upgradeCost.ToString();
        pickupRangeUpgradeCost.text = playerAttributes.pickupRange.upgradeCost.ToString();
        cooldownRecoveryUpgradeCost.text = playerAttributes.cooldownRecovery.upgradeCost.ToString();
    }

    private void SetUpgradeStats()
    {
        healthUpgradeButton.text = "Max Health +" + playerAttributes.health.increasePerLevel.ToString("F0");
        attackPowerUpgradeButton.text = "Attack Power +" + playerAttributes.attackPower.increasePerLevel.ToString("F0");
        defenseRatingUpgradeButton.text = "Defense Rating +" +
                                          playerAttributes.defenseRating.increasePerLevel.ToString("P0");
        pickupRangeUpgradeButton.text =
            "Pickup Range +" + playerAttributes.pickupRange.increasePerLevel.ToString("0.#");
        cooldownRecoveryUpgradeButton.text = "Cooldown Recovery +" +
                                             playerAttributes.cooldownRecovery.increasePerLevel.ToString("P0");
        foreach (var buttonAttribute in buttonToAttribute)
        {
            buttonAttribute.Key.interactable = playerContainer.IsAttributeBuyable(buttonAttribute.Value);
        }
    }

    private void SetCurrentStats()
    {
        playerHealth.text = playerAttributes.health.initial.ToString("F0");
        playerAttackPower.text = playerAttributes.attackPower.initial.ToString("F0");
        playerDefenseRating.text = playerAttributes.defenseRating.initial.ToString("P0");
        playerSpeed.text = playerAttributes.pickupRange.initial.ToString("0.#");
        playerCooldownRecovery.text = playerAttributes.cooldownRecovery.initial.ToString("P0");
        playerGold.text = playerStats.gold.actual.ToString("F0");
    }
}