using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text healthText;
    public TMP_Text deathText;
    public TMP_Text xpText;
    public TMP_Text goldText;
    public TMP_Text levelText;
    private GameObject player; 
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(UpdateUI());

    }

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (player && player.TryGetComponent(out PlayerUnit playerUnit))
            {
                healthText.text = playerUnit.attributes.health.actual.ToString() + "/" + playerUnit.attributes.health.initial.ToString();
                goldText.text = playerUnit.stats.gold.actual.ToString();
                xpText.text = playerUnit.stats.xp.actual.ToString() + "/" + playerUnit.stats.xp.max.ToString();
                levelText.text = playerUnit.stats.level.actual.ToString();
            }
        }
    }
}
