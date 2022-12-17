using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Energy : Collectible
{
    public int amount;
    public Sprite[] sprites;

    protected override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public static event Action<int> OnEnergyCollected;

    protected override void OnPlayerReach()
    {
        OnEnergyCollected?.Invoke(amount);
        Destroy(gameObject);
    }
}