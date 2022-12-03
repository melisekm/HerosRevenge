using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static event Action OnAttack;
    public static event Action<int> OnSwitchAbility;
    public static event Action<float, float> OnMovement;
    public static event Action OnUltimateButtonPress;

    private int maxAbilityCount;
    private bool isActive = true;


    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += DisableControls;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= DisableControls;
    }

    private void Start()
    {
        maxAbilityCount = TryGetComponent(out AbilityStash stash) ? stash.defaultAbilityTypes.Count : 0;
    }

    private void DisableControls()
    {
        isActive = false;
    }

    private void Update()
    {
        if (!isActive) return;

        OnMovement?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButton("Fire1"))
        {
            OnAttack?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnUltimateButtonPress?.Invoke();
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