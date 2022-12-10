using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinConditionChecker : MonoBehaviour
{
    public static event Action OnWinConditionMet;
    public string description;

    protected void InvokeWin()
    {
        OnWinConditionMet?.Invoke();
    }
}