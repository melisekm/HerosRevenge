using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UltimateUI : MonoBehaviour
{
    private AbilityHolder ultimateHolder;
    private float currentMaxCooldown;
    public TMP_Text cdText;
    public Image darkMask;
    public Image abilityIcon;

    private void OnEnable()
    {
        AbilityStash.OnUltimateChanged += Initialize;
    }

    private void OnDisable()
    {
        AbilityStash.OnUltimateChanged -= Initialize;
        ultimateHolder.OnAbilityReady -= ShowReady;
        ultimateHolder.OnUltimateUsed -= ShowCooldown;
    }

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player && player.TryGetComponent(out AbilityStash abilityStash))
        {
            ultimateHolder = abilityStash.ultimateAbilityHolder;
            // we only want to subscribe to this particular abilityholder
            ultimateHolder.OnAbilityReady += ShowReady;
            ultimateHolder.OnUltimateUsed += ShowCooldown;
        }
    }

    private void Initialize(ScriptableAbility scriptableAbility)
    {
        if (scriptableAbility)
        {
            abilityIcon.enabled = true;
            abilityIcon.sprite = scriptableAbility.icon;
            cdText.gameObject.SetActive(true);
        }
        else
        {
            abilityIcon.enabled = false;
            cdText.gameObject.SetActive(false);
        }
    }

    private void ShowCooldown()
    {
        darkMask.fillAmount = 1;
        currentMaxCooldown = ultimateHolder.cooldownTime;
        cdText.text = currentMaxCooldown.ToString("F0");
    }

    private void ShowReady()
    {
        cdText.text = "R";
    }

    private void Update()
    {
        if (ultimateHolder.cooldownTime > 0)
        {
            // percentage dark mask fillamount
            darkMask.fillAmount = ultimateHolder.cooldownTime / currentMaxCooldown;
            cdText.text = ultimateHolder.cooldownTime.ToString("F0");
        }
    }
}