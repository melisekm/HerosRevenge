using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public Unit enemyUnit;
    private Transform bar;
    private Transform background;

    private void Start()
    {
        bar = transform.Find("Bar");
        background = transform.Find("Background");
    }

    private void Update()
    {
        var currentHealth = enemyUnit.attributes.health.actual;
        var maxCurrentHealth = enemyUnit.attributes.health.initial;
        background.gameObject.SetActive(currentHealth < maxCurrentHealth); // TODO move somewhere else or remove
        bar.gameObject.SetActive(currentHealth < maxCurrentHealth); // Shouldnt really update this all the time


        SetSize(currentHealth / maxCurrentHealth);
    }

    private void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}