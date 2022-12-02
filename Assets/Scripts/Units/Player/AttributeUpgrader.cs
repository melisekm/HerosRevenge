using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
public class AttributeUpgrader : MonoBehaviour
{
    private List<AttributeUpgrade> upgrades;
    private PlayerUnit playerUnit;

    private void OnEnable()
    {
        LevelUpUISetter.OnRewardSelected += UpdateAttributes;
    }

    private void OnDisable()
    {
        LevelUpUISetter.OnRewardSelected -= UpdateAttributes;
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

    private void UpdateAttributes(ScriptableReward scriptableStat)
    {
        if (scriptableStat is ScriptableStatUpgrade statUpgrade)
        {
            upgrades[(int)statUpgrade.statType].ApplyUpgrade(statUpgrade.amount);
        }
    }
}

public class AttributeUpgrade
{
    protected Attribute attribute;


    public AttributeUpgrade(Attribute attribute)
    {
        this.attribute = attribute;
    }

    public virtual void ApplyUpgrade(float amount)
    {
        attribute.initial += amount;
        attribute.actual += amount;
    }
}

public class GoldAttributeUpgrade : AttributeUpgrade
{
    public GoldAttributeUpgrade(Attribute attribute) : base(attribute)
    {
    }

    public override void ApplyUpgrade(float amount)
    {
        attribute.actual += amount;
    }
}