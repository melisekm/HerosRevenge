using TMPro;
using UnityEngine;

public class GameFinishedUI : MonoBehaviour
{
    public TMP_Text level;
    public TMP_Text gold;
    public TMP_Text arenas;
    public TMP_Text killCount;
    public TMP_Text timePlayed;
    public TMP_Text deaths;
    public TMP_Text health;
    public TMP_Text damage;
    public TMP_Text speed;
    public TMP_Text defense;
    public TMP_Text cdRecovery;
    public TMP_Text pickupRange;

    private void Start()
    {
        var playerContainerGo = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerGo && playerContainerGo.TryGetComponent(out PlayerContainer playerContainer))
        {
            var playerAttributes = playerContainer.playerAttributes;
            var playerStats = playerContainer.playerStats;
            level.text = playerStats.level.actual.ToString("F0");
            gold.text = playerStats.gold.actual.ToString("F0");
            arenas.text = playerContainer.arenas.Count.ToString("F0");
            killCount.text = playerContainer.killCount.ToString("F0");
            timePlayed.text = HudSetter.FormatTime(Time.time);
            deaths.text = playerContainer.deathsCount.ToString("F0");
            health.text = playerAttributes.health.actual.ToString("F0");
            damage.text = playerAttributes.attackPower.actual.ToString("F0");
            speed.text = playerAttributes.speed.actual.ToString("0.#");
            defense.text = playerAttributes.defenseRating.actual.ToString("P0");
            cdRecovery.text = playerAttributes.cooldownRecovery.actual.ToString("P0");
            pickupRange.text = playerAttributes.pickupRange.actual.ToString("0.#");
        }
    }
}