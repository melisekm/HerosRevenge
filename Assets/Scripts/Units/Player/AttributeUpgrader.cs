using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
public class AttributeUpgrader : MonoBehaviour
{
    private List<AttributeUpgrade> upgrades;
    private PlayerUnit playerUnit;

    private void OnEnable()
    {
        LevelUpUISetter.OnRewardSelected += UpgradeAttribute;
        StatPowerup.OnStatUpgradeActivated += UpgradeAttribute;
        StatPowerup.OnStatUpgradeDeactivated += DowngradeAttribute;
    }

    private void OnDisable()
    {
        LevelUpUISetter.OnRewardSelected -= UpgradeAttribute;
        StatPowerup.OnStatUpgradeActivated -= UpgradeAttribute;
        StatPowerup.OnStatUpgradeDeactivated -= DowngradeAttribute;
    }

    private void Start()
    {
        playerUnit = GetComponent<PlayerUnit>();
        upgrades = new List<AttributeUpgrade>
        {
            new(playerUnit.attributes.health),
            new(playerUnit.attributes.attackPower),
            new(playerUnit.attributes.speed),
            new(playerUnit.attributes.defenseRating),
            new(playerUnit.attributes.cooldownRecovery),
            new(playerUnit.attributes.pickupRange),
            new GoldAttributeUpgrade(playerUnit.stats.gold)
        };
    }

    private void UpgradeAttribute(ScriptableReward scriptableStat)
    {
        if (scriptableStat is ScriptableStatUpgrade statUpgrade)
        {
            upgrades[(int)statUpgrade.statType].ApplyUpgrade(statUpgrade.amount, 1);
        }
    }

    private void DowngradeAttribute(ScriptableStatUpgrade scriptableStat)
    {
        upgrades[(int)scriptableStat.statType].ApplyUpgrade(scriptableStat.amount, -1);
    }
}

public class AttributeUpgrade
{
    protected Attribute attribute;

    public AttributeUpgrade(Attribute attribute)
    {
        this.attribute = attribute;
    }

    public virtual void ApplyUpgrade(float amount, int modifier)
    {
        attribute.ToggleUpgrade(amount, modifier);
    }
}

public class GoldAttributeUpgrade : AttributeUpgrade
{
    public GoldAttributeUpgrade(Attribute attribute) : base(attribute)
    {
    }

    public override void ApplyUpgrade(float amount, int _)
    {
        attribute.actual += amount;
    }
}