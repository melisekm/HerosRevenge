using System;
using UnityEngine;

public abstract class WinConditionChecker : MonoBehaviour
{
    public string description;
    private bool isWinConditionMet;
    protected int newGamePlus;

    protected virtual void Awake()
    {
        var playerContainerGo = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerGo && playerContainerGo.TryGetComponent(out PlayerContainer playerContainer))
        {
            newGamePlus = playerContainer.newGamePlus;
        }
    }

    public static event Action OnWinConditionMet;

    protected void InvokeWin()
    {
        if (!isWinConditionMet)
        {
            isWinConditionMet = true;
            OnWinConditionMet?.Invoke();
        }
    }
}