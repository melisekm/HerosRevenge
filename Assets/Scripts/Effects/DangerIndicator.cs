using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DangerIndicator : Effect
{
    private DangerIndicatorEffect effect;
    private Transform progressCircle;
    private float activationTime = 3f;
    private float timeElapsed;

    private void Start()
    {
        progressCircle = transform.Find("ProgressCircle");
        transform.localScale = effect.ability.GetComponent<CircleCollider2D>().radius * 2 * Vector3.one;
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
                damage = effect.damage,
                range = 0,
            };
            ability.Activate(abilityStats, transform.position, Faction.Player);
            // destroy the indicator
            Destroy(gameObject);
        }
    }

    public override void Initialize(ScriptableEffect scriptableEffect)
    {
        effect = (DangerIndicatorEffect) scriptableEffect;
    }
}