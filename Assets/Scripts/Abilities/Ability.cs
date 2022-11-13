using System;
using UnityEngine;
using Utils;

public abstract class Ability : MonoBehaviour
{
    public AbilityStats abilityStats { get; private set; }

    protected Vector3 target;
    protected Faction targetFaction;
    protected int rotationOffset = -90;
    public bool isPiercing = false;
    public bool collidesWithSolidObjects = true;
    
    public virtual void SetAbilityStats(AbilityStats st) => abilityStats = st;


    protected virtual void Start()
    {
        float angle = GetAngleToTarget();
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    public void SetTarget(Vector3 target, Faction targetFaction)
    {
        this.target = target;
        this.targetFaction = targetFaction;
    }
    
    protected float GetAngleToTarget()
    {
        // https://answers.unity.com/questions/995540/move-towards-mouse-direction-infinitely-at-constan.html
        Vector3 targetPos = new Vector3(target.x, target.y, 0);
        Vector3 diff = targetPos - transform.position;
        diff.Normalize();
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (targetFaction == unit.faction)
            {
                unit.TakeDamage(abilityStats.damage);
                if (!isPiercing)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (collidesWithSolidObjects && collision.gameObject.CompareTag("SolidObjects"))
        {
            Destroy(gameObject);
        }
    }

  
}