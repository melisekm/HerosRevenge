using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool isActive = true;

    private int maxAbilityCount;

    private void Start()
    {
        maxAbilityCount = TryGetComponent(out AbilityStash stash) ? stash.defaultAbilityTypes.Count : 0;
    }

    private void Update()
    {
        if (!isActive) return;

        OnMovement?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButton("Fire1"))
        {
            OnAttack?.Invoke();
        }

        if (Input.GetButtonDown("Ultimate"))
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


    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += DisableControls;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= DisableControls;
    }

    public static event Action OnAttack;
    public static event Action<int> OnSwitchAbility;
    public static event Action<float, float> OnMovement;
    public static event Action OnUltimateButtonPress;

    private void DisableControls()
    {
        isActive = false;
    }
}