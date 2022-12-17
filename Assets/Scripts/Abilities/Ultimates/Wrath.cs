using System;
using UnityEngine;

public class Wrath : Ability, IUltimate
{
    public float duration;
    [Header("Attribute boosts")] public float health = 20f;
    public float attackPower = 10f;
    public float speed = 2f;
    public float coolDownRecovery = 0.1f;
    public float defenseRating = 0.2f;
    public float pickupRange = 2f;
    private PlayerUnit playerUnit;
    private IUltimate ultimate;
    public float timer { get; set; }

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

        ultimate = this;
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        timer = duration;
        ultimate.ActivateUltimate();
    }

    private void OnDestroy()
    {
        ToggleBoost(playerUnit.attributes, -1);
        timer = -1;
        ultimate.ActivateUltimate();
    }

    private void ToggleBoost(Attributes attributes, int modifier)
    {
        attributes.health.ToggleUpgrade(health, modifier);
        attributes.attackPower.ToggleUpgrade(attackPower, modifier);
        attributes.speed.ToggleUpgrade(speed, modifier);
        attributes.cooldownRecovery.ToggleUpgrade(coolDownRecovery, modifier);
        attributes.defenseRating.ToggleUpgrade(defenseRating, modifier);
        attributes.pickupRange.ToggleUpgrade(pickupRange, modifier);
    }
}