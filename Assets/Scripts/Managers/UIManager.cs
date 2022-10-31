using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text healthText;
    public TMP_Text deathText;
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
                healthText.text = playerUnit.attributes.health.actual.ToString();
            }
        }
    }
}
