using System;
using UnityEngine;

public abstract class WinConditionChecker : MonoBehaviour
{
    public string description;
    private bool isWinConditionMet;
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