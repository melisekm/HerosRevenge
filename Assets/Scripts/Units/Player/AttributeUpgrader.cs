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
            new GoldAttributeUpgrade(playerUnit.stats.gold)
        };
    }

    public void Upgrade(StatType statType, float amount)
    {
        upgrades[(int)statType].ApplyUpgrade(amount);
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
