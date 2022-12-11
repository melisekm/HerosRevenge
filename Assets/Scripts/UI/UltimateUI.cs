using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UltimateUI : AbilityHolderUI
{
    private AbilityHolder abilityHolder;

    private float currentMaxCooldown;
    public TMP_Text cdText;
    public Image darkMask;


    protected override void OnDisable()
    {
        base.OnDisable();
        abilityHolder.OnAbilityReady -= ShowReady;
        abilityHolder.OnUltimateUsed -= ShowCooldown;
    }

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player && player.TryGetComponent(out AbilityStash abilityStash))
        {
            abilityHolder = abilityStash.ultimateAbilityHolder;
            // we only want to subscribe to this particular abilityholder
            abilityHolder.OnAbilityReady += ShowReady;
            abilityHolder.OnUltimateUsed += ShowCooldown;
        }
    }

    protected override void Initialize(ScriptableAbility scriptableAbility, string index)
    {
        if (keyString != index) return;
        base.Initialize(scriptableAbility, index);
        cdText.gameObject.SetActive(scriptableAbility != null);
    }

    private void ShowCooldown()
    {
        darkMask.fillAmount = 1;
        currentMaxCooldown = abilityHolder.cooldownTime;
        cdText.text = currentMaxCooldown.ToString("F0");
    }

    private void ShowReady()
    {
        cdText.text = keyString;
    }

    private void Update()
    {
        if (abilityHolder.cooldownTime > 0)
        {
            // percentage dark mask fillamount
            darkMask.fillAmount = abilityHolder.cooldownTime / currentMaxCooldown;
            cdText.text = abilityHolder.cooldownTime.ToString("F0");
        }
    }
}