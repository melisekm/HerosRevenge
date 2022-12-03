using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Treasure : Collectible
{
    public float amount = 25;
    public TMP_Text numberPopup;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private static readonly int Collected = Animator.StringToHash("Collected");

    public static event Action<float> OnTreasueCollected;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // random sprite
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        base.Start();
    }


    public override void PickUp()
    {
        OnTreasueCollected?.Invoke(amount);
        pickedUp = true;
        animator.SetTrigger(Collected);
    }

    public void Initialize(ScriptableStatPowerUp scriptableStatPowerUp)
    {
        // random amount
        if (scriptableStatPowerUp.randomAmount)
        {
            amount = Random.Range(scriptableStatPowerUp.minRandomAmount, scriptableStatPowerUp.maxRandomAmount);
            // to int
            amount = Mathf.RoundToInt(amount);
        }
        else
        {
            amount = scriptableStatPowerUp.statUpgrade.amount;
        }

        numberPopup.text = "+" + amount;
    }

    // called by animation event
    public void Die()
    {
        Destroy(gameObject);
    }
}