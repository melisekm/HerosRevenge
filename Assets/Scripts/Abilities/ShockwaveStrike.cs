using UnityEngine;

public class ShockwaveStrike : MultiProjectileAbility
{
    public ShockwaveExplosion shockwaveExplosion;
    public float spawnExplosionsTimer = 0.1f;
    public float explosionDamageMultiplier = 0.3f;
    private float timer;
    private float explosionDamage;
    private bool canSpawnExplosions = true;

    private void Start()
    {
        explosionDamage = abilityStats.damage * explosionDamageMultiplier;
    }

    protected override void Update()
    {
        base.Update();
        if (!canSpawnExplosions) return;

        timer += Time.deltaTime;
        if (timer >= spawnExplosionsTimer)
        {
            // instantiate little bit behind the gameobject
            var explosion = Instantiate(shockwaveExplosion, transform.position + transform.up, Quaternion.identity);
            explosion.Initialize(explosionDamage, targetFaction);
            timer = 0;
        }
    }

    protected override void Die()
    {
        canMove = false;
        canSpawnExplosions = false;
        animator.SetTrigger(PlayFinished);
        if (hitEffect)
        {
            hitEffect.Activate(transform.position + transform.up);
        }
    }

    // this is called from the animation event
    public void Disappear()
    {
        Destroy(gameObject);
    }
}