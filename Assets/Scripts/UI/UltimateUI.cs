using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UltimateUI : MonoBehaviour
{
    private float cooldown;
    private float baseCooldown;
    public TMP_Text cdText;
    public Image darkMask;
    private Image abilityIcon;
    private AbilityHolder ultimateHolder;

    private void Start()
    {
        abilityIcon = GetComponent<Image>();
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player && player.TryGetComponent(out AbilityStash abilityStash))
        {
            ultimateHolder = abilityStash.ultimateAbilityHolder;
        }
        abilityIcon.enabled = false;
    }

    private void OnEnable()
    {
        AbilityHolder.OnUltimateUsed += ActivateUltimate;
        AbilityStash.OnUltimateChanged += Initialize;
    }

    private void OnDisable()
    {
        AbilityHolder.OnUltimateUsed -= ActivateUltimate;
        AbilityStash.OnUltimateChanged -= Initialize;
    }

    private void Initialize()
    {
        abilityIcon.enabled = true;
        var scriptableAbility = ultimateHolder.scriptableAbility;
        abilityIcon.sprite = scriptableAbility.icon;
        baseCooldown = scriptableAbility.stats.baseCooldown;
    }

    private void ActivateUltimate()
    {
        darkMask.gameObject.SetActive(true);
        cdText.gameObject.SetActive(true);
        darkMask.fillAmount = 1;
        cdText.text = cooldown.ToString("F0");
        cooldown = baseCooldown;
    }


    private void Update()
    {
        if (cooldown <= 0)
        {
            darkMask.gameObject.SetActive(false);
            cdText.gameObject.SetActive(false);
            return;
        }
        // percentage dark mask fillamount
        darkMask.fillAmount = cooldown / baseCooldown;
        cdText.text = cooldown.ToString("F0");
        cooldown -= Time.deltaTime;
        
        
    }
}