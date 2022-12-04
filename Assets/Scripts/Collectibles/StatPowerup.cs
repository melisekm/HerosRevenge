using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPowerup : Powerup
{
    private ScriptableStatUpgrade statUpgrade;
    public static event Action<ScriptableStatUpgrade> OnStatUpgradeActivated;
    public static event Action<ScriptableStatUpgrade> OnStatUpgradeDeactivated;

    // public static event Action<ScriptableStatUpgrade> OnStatUpgrade;
    public override void Initialize(ScriptablePowerUp powerup)
    {
        var scriptableStatPowerup = (ScriptableStatPowerUp)powerup;
        statUpgrade = scriptableStatPowerup.statUpgrade;
    }

    public override void PickUp()
    {
        OnStatUpgradeActivated?.Invoke(statUpgrade);
        spriteRenderer.enabled = false;
    }

    protected override void Update()
    {
        if (pickedUp)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
            }
            else
            {
                OnStatUpgradeDeactivated?.Invoke(statUpgrade);
                Destroy(gameObject);
            }
        }
        else
        {
            base.Update();
            
        }
    }
}