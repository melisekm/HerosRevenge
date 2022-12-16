using DigitalRuby.LightningBolt;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public float destroyAfterSeconds = 0.3f;

    private void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void Initialize(Vector3 start, Vector3 end)
    {
        var bolt = GetComponentInChildren<LightningBoltScript>();
        bolt.StartPosition = start;
        bolt.EndPosition = end;
    }
}