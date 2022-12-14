using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Treasure : Collectible, INitializableCollectible
{
    private static readonly int Collected = Animator.StringToHash("Collected");
    public float amount = 25;
    public TMP_Text numberPopup;
    public Sprite[] sprites;

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        // random sprite
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void Initialize(ScriptablePowerUp powerup, float disappearTime)
    {
        var scriptableStatPowerUp = (ScriptableStatPowerUp)powerup;
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

    public static event Action<float> OnTreasueCollected;


    public override void PickUp()
    {
        base.PickUp();
        OnTreasueCollected?.Invoke(amount);
        animator.SetTrigger(Collected);
    }

    // called by animation event
    public void Die()
    {
        Destroy(gameObject);
    }
}