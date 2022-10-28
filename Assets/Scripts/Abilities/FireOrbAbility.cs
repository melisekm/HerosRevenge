using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireOrbAbility : Ability
{
    private Vector3 mousePos;
    public float speed = 10f;
    public int rotationOffset = -90;
    private float range;

    private float flownDistance;
    // private Rigidbody2D rb;
    // private Vector3 mousePos;

    private void Start()
    {
        range = abilityStats.range;
        // rb = GetComponent<Rigidbody2D>();


        // https://answers.unity.com/questions/995540/move-towards-mouse-direction-infinitely-at-constan.html
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 diff = mousePos - transform.position;
        diff.Normalize();
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    private void Update()
    {
        // move towards mouse + range
        transform.position += transform.up * (speed * Time.deltaTime);
        

        flownDistance += speed * Time.deltaTime;
        if (flownDistance >= range)
        {
            Destroy(gameObject);
        }
    }
}