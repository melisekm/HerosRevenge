using UnityEngine;

public class BloodAura : GroundAbility, IUltimate
{
    private Transform playerPos;
    private IUltimate ultimate;
    public float timer { get; set; }


    protected override void Start()
    {
        base.Start();
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            playerPos = player.transform;
        }

        ultimate = this;
    }

    protected override void Update()
    {
        transform.position = playerPos.position;
        base.Update();
        timer = dissapearTime;
        ultimate.ActivateUltimate();
    }

    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += Disable;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= Disable;
    }

    private void OnDestroy()
    {
        timer = -1;
        ultimate.ActivateUltimate();
    }

    private void Disable()
    {
        isActive = false;
    }
}