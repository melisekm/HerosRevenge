using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAbility : Ability
{
    public float speed = 10f;
    public int rotationOffset = -90;
    private float flownDistance;
    public bool isPiercing = false;
    public bool collidesWithSolidObjects = true;

    private float GetAngleToTarget()
    {
        // https://answers.unity.com/questions/995540/move-towards-mouse-direction-infinitely-at-constan.html
        Vector3 targetPos = new Vector3(target.x, target.y, 0);
        Vector3 diff = targetPos - transform.position;
        diff.Normalize();
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }

    protected virtual void Start()
    {
        float angle = GetAngleToTarget();
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    protected virtual void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);

        flownDistance += speed * Time.deltaTime;
        if (flownDistance >= abilityStats.range)
        {
            Destroy(gameObject);
        }
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