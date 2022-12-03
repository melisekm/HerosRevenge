using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public Color hitColor;

    public void Activate(Vector3 position)
    {
        var hitEffectMain = hitEffect.main;
        hitEffectMain.startColor = new ParticleSystem.MinMaxGradient(hitColor);
        Instantiate(hitEffect, position, Quaternion.identity);
    }
}
