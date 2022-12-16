using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    private Attribute health;
    private float timer;
    private float regenTime = 0.2f;
    private float regenAmount = 1f;
    private bool active = true;

    private void Start()
    {
        var unit = GetComponent<Unit>();
        if (unit)
        {
            health = unit.attributes.health;
        }
    }

    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += DisableHealthRegen;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= DisableHealthRegen;
    }

    private void DisableHealthRegen()
    {
        active = false;
    }

    private void Update()
    {
        if (!active) return;
        timer += Time.deltaTime;
        if (timer >= regenTime && health.actual < health.initial)
        {
            timer = 0;
            health.actual += regenAmount;
        }
    }
}