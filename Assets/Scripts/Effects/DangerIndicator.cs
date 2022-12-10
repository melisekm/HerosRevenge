using UnityEngine;

public class DangerIndicator : Effect
{
    private DangerIndicatorEffect effect;
    private Transform progressCircle;
    private float activationTime = 3f;

    [Header("Random Activation Time")]
    public float minActivationTime = 1f;
    public float maxActivationTime = 4f;

    private float timeElapsed;
    private Faction targetFaction;
    private float explosionRadius = 5f;

    private void Start()
    {
        progressCircle = transform.Find("ProgressCircle");
        activationTime = Random.Range(minActivationTime, maxActivationTime);
    }

    public override void Initialize(ScriptableEffect scriptableEffect, float radius, Faction targetFaction)
    {
        effect = (DangerIndicatorEffect)scriptableEffect;
        this.targetFaction = targetFaction;
        explosionRadius = radius;
        transform.localScale = explosionRadius * Vector3.one;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        // percentage of activation time
        float percentage = timeElapsed / activationTime;
        // set the progress circle to the percentage
        progressCircle.localScale = new Vector3(percentage, percentage, percentage);
        if (timeElapsed >= activationTime)
        {
            // activate the ability
            Ability ability = Instantiate(effect.ability, transform.position, Quaternion.identity);
            ability.transform.localScale *= explosionRadius / 2;
            AbilityStats abilityStats = new AbilityStats
            {
                damage = effect.damage, // scale with level
            };
            ability.Activate(abilityStats, transform.position, targetFaction);
            // destroy the indicator
            Destroy(gameObject);
        }
    }
}