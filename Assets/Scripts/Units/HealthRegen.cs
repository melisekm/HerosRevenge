using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    private bool active = true;
    private Attribute health;
    private float timer;
    public float regenTime = 0.2f;
    public float regenAmount = 1f;

    private void Start()
    {
        var unit = GetComponent<Unit>();
        if (unit)
        {
            health = unit.attributes.health;
        }
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
}