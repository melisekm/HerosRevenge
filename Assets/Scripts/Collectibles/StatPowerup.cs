using System;
using UnityEngine;

public class StatPowerup : Powerup
{
    private ScriptableStatUpgrade statUpgrade;

    protected override void Start()
    {
        base.Start();
        spriteRenderer.sprite = statUpgrade.icon;
    }

    protected override void Update()
    {
        if (isActive)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            base.Update();
        }
    }

    public void OnDestroy()
    {
        if (pickedUp)
        {
            OnStatUpgradeDeactivated?.Invoke(statUpgrade);
        }
    }

    public static event Action<ScriptableStatUpgrade> OnStatUpgradeActivated;
    public static event Action<ScriptableStatUpgrade> OnStatUpgradeDeactivated;

    // public static event Action<ScriptableStatUpgrade> OnStatUpgrade;
    public override void Initialize(ScriptablePowerUp powerup, float disappearTime)
    {
        this.disappearTime = disappearTime;
        var scriptableStatPowerup = (ScriptableStatPowerUp)powerup;
        statUpgrade = scriptableStatPowerup.statUpgrade;
    }

    protected override void OnPlayerReach()
    {
        isActive = true;
        spriteRenderer.enabled = false;
        OnStatUpgradeActivated?.Invoke(statUpgrade);
    }
}