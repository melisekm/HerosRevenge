using System;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public bool isFacingRight = true;
    private Action rotateToPlayer;
    private SpriteRenderer sprite;
    private GameObject player;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        
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