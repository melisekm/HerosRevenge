using System;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public bool isFacingRight = true;
    private GameObject player;
    private Action rotateToPlayer;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");

        if (isFacingRight)
        {
            rotateToPlayer = () =>
                sprite.flipX = player.transform.position.x < transform.position.x;
        }
        else
        {
            rotateToPlayer = () =>
                sprite.flipX = player.transform.position.x > transform.position.x;
        }
    }

    private void Update()
    {
        rotateToPlayer();
    }
}