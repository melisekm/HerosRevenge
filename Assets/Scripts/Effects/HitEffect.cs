using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public Color hitColor;

    public void Activate()
    {
        var hitEffectMain = hitEffect.main;
        hitEffectMain.startColor = new ParticleSystem.MinMaxGradient(hitColor);
        Instantiate(hitEffect, transform.position + transform.up, Quaternion.identity);
    }
}
