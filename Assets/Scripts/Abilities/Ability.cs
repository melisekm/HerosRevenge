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
    protected Animator animator;
    protected HitEffect hitEffect;


    public virtual void SetAbilityStats(AbilityStats st) => abilityStats = st;


    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        hitEffect = GetComponent<HitEffect>();
    }

    protected virtual void Start()
    {
        float angle = GetAngleToTarget();
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    public void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        SetAbilityStats(stats);
        SetTarget(target, targetFaction);
        SetLayer(targetFaction);
        enabled = true;
    }

    private void SetLayer(Faction targetFaction)
    {
        if (targetFaction == Faction.Player)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyAbilityLayer"); // TODO: FIXME do we do this like this?
        }
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
                    Die();
                }
            }
        }

        if (collidesWithSolidObjects && collision.gameObject.CompareTag("SolidObjects"))
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}