using System.Linq;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected static readonly int PlayFinished = Animator.StringToHash("playFinished");
    public bool collidesWithUnits;
    public bool collidesWithEnvironment = true;
    protected Animator animator;
    protected HitEffect hitEffect;

    protected Vector3 target;
    protected Faction targetFaction;
    public AbilityStats abilityStats { get; private set; }


    protected void Awake()
    {
        animator = GetComponent<Animator>();
        hitEffect = GetComponent<HitEffect>();
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Act(collision);
    }

    protected void SetAbilityStats(AbilityStats st) => abilityStats = st;


    public virtual void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        SetAbilityStats(stats);
        SetTarget(target, targetFaction);
        SetLayer(targetFaction);
    }

    protected virtual void SetLayer(Faction targetFaction)
    {
        if (targetFaction == Faction.Player)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyAbilityLayer"); // TODO: FIXME do we do this like this?
        }
    }

    protected void SetTarget(Vector3 target, Faction targetFaction)
    {
        this.target = target;
        this.targetFaction = targetFaction;
    }

    protected virtual void Act(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (targetFaction == unit.faction)
            {
                unit.TakeDamage(abilityStats.damage);
                if (collidesWithUnits)
                {
                    Die();
                }
            }
        }

        if (collidesWithEnvironment && collision.gameObject.CompareTag("SolidObjects"))
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (animator && animator.parameters.Any(p => p.type == AnimatorControllerParameterType.Trigger))
        {
            // if trigger parameter exists in animator
            animator.SetTrigger(PlayFinished);
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            if (hitEffect)
            {
                hitEffect.Activate(transform.position + transform.up);
            }

            Destroy(gameObject);
        }
    }
}