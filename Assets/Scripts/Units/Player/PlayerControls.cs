using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static event Action OnAttack;
    public static event Action<int> OnSwitchAbility;
    public static event Action<float, float> OnMovement;

    private int maxAbilityCount;

    private void Start()
    {
        maxAbilityCount = TryGetComponent(out AbilityStash stash) ? stash.maxAbilityCount : 0;
    }


    private void Update()
    {
        OnMovement?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // rewrite to GetButtonDown

        if (Input.GetButton("Fire1"))
        {
            OnAttack?.Invoke();
        }

        // iterate through the keys 1, 2.. and check if they are pressed
        for (int i = 0; i < maxAbilityCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                OnSwitchAbility?.Invoke(i);
            }
        }
    }
}