using UnityEngine;

public class Wrath : Ability, IUltimateEventInvokable
{
    public float duration;
    [Header("Attribute boosts")] public float health = 20f;
    public float attackPower = 10f;
    public float speed = 2f;
    public float coolDownRecovery = 0.1f;
    public float defenseRating = 0.2f;
    public float pickupRange = 2f;
    private PlayerUnit playerUnit;
    private IUltimateEventInvokable ultimateEventInvokable;

    private void Start()
    {
        GameObject playerGo = GameObject.FindWithTag("Player");
        if (playerGo && playerGo.TryGetComponent(out PlayerUnit playerUnit))
        {
            this.playerUnit = playerUnit;
            Attributes attributes = this.playerUnit.attributes;
            ToggleBoost(attributes, 1);
            Destroy(gameObject, duration);
        }

        ultimateEventInvokable = this;
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        ultimateEventInvokable.InvokeUltimateEvent(duration);
    }

    public void OnDestroy()
    {
        ToggleBoost(playerUnit.attributes, -1);
        ultimateEventInvokable.InvokeUltimateEvent(IUltimateEventInvokable.EndOfActivation);
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