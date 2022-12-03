using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private Unit unit;
    private Transform bar;
    private Transform background;
    private float prevHealth;
    private float prevMaxHealth;
    public bool isAlwaysActive = false;


    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void Start()
    {
        bar = transform.Find("Bar");
        background = transform.Find("Background");
        SetActive(isAlwaysActive);
    }

    private void Update()
    {
        var currentHealth = unit.attributes.health.actual;
        var maxCurrentHealth = unit.attributes.health.initial;
        bool hasHealthChanged = currentHealth != prevHealth || maxCurrentHealth != prevMaxHealth;
        if (hasHealthChanged)
        {
            if (!isAlwaysActive)
            {
                bool notFullNotZero = currentHealth < maxCurrentHealth && currentHealth > 0;
                SetActive(notFullNotZero);
            }

            prevHealth = currentHealth;
            prevMaxHealth = maxCurrentHealth;
            SetSize(currentHealth / maxCurrentHealth);
        }
    }

    private void SetActive(bool active)
    {
        background.gameObject.SetActive(active);
        bar.gameObject.SetActive(active);
    }

    private void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}