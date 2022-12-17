using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UltimateUI : AbilityHolderUI
{
    public TMP_Text cdText;
    public Image darkMask;
    public GameObject ultimateActivePanel;
    public TMP_Text ultimateActiveText;
    private AbilityHolder abilityHolder;

    private float currentMaxCooldown;
    private Image ultimateActiveImage;

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

        ultimateActiveImage = ultimateActivePanel.GetComponent<Image>();
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

    protected override void OnEnable()
    {
        base.OnEnable();
        IUltimateEventInvokable.OnUltimateActive += ShowActive;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        abilityHolder.OnAbilityReady -= ShowReady;
        abilityHolder.OnUltimateUsed -= ShowCooldown;
        IUltimateEventInvokable.OnUltimateActive -= ShowActive;
    }

    private void ShowActive(float timer)
    {
        if (timer <= 0)
        {
            ultimateActivePanel.SetActive(false);
        }
        else
        {
            ultimateActiveText.text = timer.ToString("F0");
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
        ultimateActivePanel.SetActive(true);
        ultimateActiveImage.sprite = abilityIcon.sprite;
    }

    private void ShowReady()
    {
        cdText.text = keyString;
    }
}