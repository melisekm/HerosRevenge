using UnityEngine;
using Utils;

public class DangerIndicator : Effect
{
    private DangerIndicatorEffect effect;
    private Transform progressCircle;
    public float activationTime = 3f;

    [Header("Random Activation Time")] public bool randomActivationTime = true;
    public float minActivationTime = 1f;
    public float maxActivationTime = 4f;

    private float timeElapsed;
    private Faction targetFaction;

    private void Start()
    {
        progressCircle = transform.Find("ProgressCircle");
        if (progressCircle && effect.ability.TryGetComponent(out CircleCollider2D circleCollider))
        {
            transform.localScale = circleCollider.radius * 2 * Vector3.one;
        }

        if (randomActivationTime)
        {
            activationTime = Random.Range(minActivationTime, maxActivationTime);
        }
    }

    public override void Initialize(ScriptableEffect scriptableEffect, Faction targetFaction)
    {
        effect = (DangerIndicatorEffect)scriptableEffect;
        this.targetFaction = targetFaction;
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