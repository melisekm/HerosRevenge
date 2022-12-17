using UnityEngine;

public class AbilityPowerup : Powerup
{
    private ScriptableAbility scriptableAbility;

    protected override void Start()
    {
        base.Start();
        spriteRenderer.sprite = scriptableAbility.icon;
    }

    public override void Initialize(ScriptablePowerUp powerup, float disappearTime)
    {
        this.disappearTime = disappearTime;
        var scriptableStatPowerup = (ScriptableAbilityPowerUp)powerup;
        scriptableAbility = scriptableStatPowerup.ability;
    }

    protected override void OnPlayerReach()
    {
        Ability ability = Instantiate(scriptableAbility.prefab, transform.position, Quaternion.identity);
        AbilityStats abilityStats = scriptableAbility.stats;
        abilityStats.damage += playerUnit.attributes.attackPower.actual;
        ability.Activate(abilityStats, transform.position, Faction.Enemy);
        Destroy(gameObject);
    }
}