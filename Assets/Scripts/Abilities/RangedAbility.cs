using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAbility : Ability
{
    public float speed = 10f;
    public int rotationOffset = -90;
    private float flownDistance;
    public bool isPiercing = false;

    private float GetAngleToMouse()
    {
        // https://answers.unity.com/questions/995540/move-towards-mouse-direction-infinitely-at-constan.html
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 diff = mousePos - transform.position;
        diff.Normalize();
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }

    protected virtual void Start()
    {
        float angle = GetAngleToMouse();
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
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            collision.gameObject.GetComponent<Unit>().TakeDamage(abilityStats.damage);
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("SolidObjects"))
        {
            Debug.Log("Hit Solid Object");
            Destroy(gameObject);
        }
    }
}