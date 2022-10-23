using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Stats stats;
    
    protected virtual void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    protected virtual void OnDestroy()
    {
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }

    protected virtual void OnStateChanged(GameState state)
    {
        
    }
    
    public virtual void TakeDamage(int damage)
    {
        
    }
}
