using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : Singleton<Systems>
{
    // persistent singleton
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
