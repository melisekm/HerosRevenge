using UnityEngine;

public class Wrath : Ability
{
    private PlayerUnit playerUnit;
    public float duration;
    [Header("Attribute boosts")] public float health = 20f;
    public float attackPower = 10f;
    public float speed = 2f;
    public float coolDownRecovery = 0.1f;
    public float defenseRating = 0.2f;
    public float pickupRange = 2f;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player && player.TryGetComponent(out PlayerUnit pu))
        {
            playerUnit = pu;
            Attributes attributes = playerUnit.attributes;
            ToggleBoost(attributes, 1);
            Destroy(gameObject, duration);
        }
    }

    private void OnDestroy()
    {
        ToggleBoost(playerUnit.attributes, -1);
    }

    private void ToggleBoost(Attributes attributes, int modifier)
    {
        ToggleAttributeBoost(attributes.health, health, modifier);
        // special case when losing boost would kill player, set to 1 instead
        if (attributes.health.actual <= attributes.health.min)
            attributes.health.actual = 1; // this wont ressurect him
        ToggleAttributeBoost(attributes.attackPower, attackPower, modifier);
        ToggleAttributeBoost(attributes.speed, speed, modifier);
        ToggleAttributeBoost(attributes.cooldownRecovery, coolDownRecovery, modifier);
        ToggleAttributeBoost(attributes.defenseRating, defenseRating, modifier);
        ToggleAttributeBoost(attributes.pickupRange, pickupRange, modifier);
    }

    private void ToggleAttributeBoost(Attribute attr, float value, int modifier)
    {
        attr.initial += value * modifier;
        float newValue = attr.actual + value * modifier;
        if (newValue <= attr.min)
        {
            attr.actual = attr.min;
        }
        else if (newValue >= attr.max)
        {
            attr.actual = attr.max;
        }
        else
        {
            attr.actual = newValue;
        }
    }
}