using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AbilityHolderUI : MonoBehaviour
{
    public Image abilityIcon;
    public string keyString = "R";
    private GameObject highlight;

    private void OnEnable()
    {
        AbilityStash.OnAbilityChanged += Initialize;
        PlayerControls.OnSwitchAbility += ShowBorder;
    }


    protected virtual void OnDisable()
    {
        AbilityStash.OnAbilityChanged -= Initialize;
        PlayerControls.OnSwitchAbility += ShowBorder;
    }

    private void Start()
    {
        highlight = transform.Find("Highlight").gameObject;
        if (highlight && keyString == "1")
        {
            highlight.SetActive(true);
        }
        else
        {
            highlight.SetActive(false);
        }
    }

    private void ShowBorder(int index)
    {
        if (highlight)
        {
            highlight.SetActive((index + 1).ToString() == keyString);
        }
    }


    protected virtual void Initialize(ScriptableAbility scriptableAbility, string index)
    {
        if (keyString != index) return;
        if (scriptableAbility)
        {
            abilityIcon.enabled = true;
            abilityIcon.sprite = scriptableAbility.icon;
        }
        else
        {
            abilityIcon.enabled = false;
        }
    }
}