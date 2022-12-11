using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AbilityHolderUI : MonoBehaviour
{
    public Image abilityIcon;
    public string keyString = "R";
    private GameObject highlight;
    private GameObject keyGo;

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
        var keyTextGo = transform.Find("KeyText");
        if (keyTextGo && keyTextGo.TryGetComponent(out TMP_Text keyText))
        {
            keyGo = keyTextGo.gameObject; 
            if (keyString != keyText.text)
            {
                Debug.LogError("KeyString and KeyText do not match");
            }
            keyString = keyText.text;
        }
        
        highlight = transform.Find("Highlight").gameObject;
        if (highlight && keyString == "1")
        {
            highlight.SetActive(true);
            keyGo.gameObject.SetActive(true);
        }
        else
        {
            highlight.SetActive(false);
            keyGo.gameObject.SetActive(false);
        }
    }

    private void ShowBorder(int index)
    {
        if (highlight && abilityIcon.enabled)
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
            if (keyGo)
                keyGo.SetActive(true);
        }
        else
        {
            abilityIcon.enabled = false;
            if (keyGo)
                keyGo.SetActive(false);
        }
    }
}