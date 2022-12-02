using System;
using System.Collections.Generic;

public class AttributeUpgrader
{
    private List<AttributeUpgrade> upgrades;

    public AttributeUpgrader(PlayerUnit playerUnit)
    {
        upgrades = new List<AttributeUpgrade>
        {
            new(playerUnit.attributes.health),
            new(playerUnit.attributes.attackPower),
            new(playerUnit.attributes.speed),
            new(playerUnit.attributes.defenseRating),
            new(playerUnit.attributes.cooldownRecovery),
            new(playerUnit.attributes.pickupRange),
            new GoldAttributeUpgrade(playerUnit.progressionController.PickUpGold)
        };
    }

    public void Upgrade(StatType statType, float amount)
    {
        upgrades[(int)statType].ApplyUpgrade(amount);
    }
}

public class AttributeUpgrade
{
    private Attribute attribute;

    protected AttributeUpgrade()
    {
    }

    public AttributeUpgrade(Attribute attribute)
    {
        this.attribute = attribute;
    }

    public virtual void ApplyUpgrade(float amount)
    {
        attribute.actual += amount;
        attribute.initial += amount;
    }
}

public class GoldAttributeUpgrade : AttributeUpgrade
{
    private Action<float> pickupGold;

    public GoldAttributeUpgrade(Action<float> pickupGold)
    {
        this.pickupGold = pickupGold;
    }

    public override void ApplyUpgrade(float amount)
    {
        pickupGold?.Invoke(amount);
    }
}
