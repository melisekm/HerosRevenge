using System;
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
    public TMP_Text timeText;
    private GameObject player;
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        GameManager.onUpdateTime += UpdateTime;
    }

    private void OnDisable()
    {
        GameManager.onUpdateTime -= UpdateTime;
    }

    private void Start()
    {
        StartCoroutine(UpdateUI());
    }

    // https://github.com/michalferko/tvorbahier/blob/master/PacMan/Assets/Scripts/UIManager.cs
    void UpdateTime(float value)
    {
        // transform time to desired format
        int minutes = Mathf.FloorToInt(value / 60F);
        int seconds = Mathf.FloorToInt(value - minutes * 60);
        string niceTime = $"{minutes:00}:{seconds:00}";

        // Set UI time text
        timeText.text = niceTime;
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
