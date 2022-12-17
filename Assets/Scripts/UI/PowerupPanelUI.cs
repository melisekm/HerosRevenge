using TMPro;
using UnityEngine;

public class PowerupPanelUI : MonoBehaviour
{
    public GameObject powerUpPanel;
    public GameObject powerUpBar;

    private void OnEnable()
    {
        StatPowerup.OnStatUpgradeActivated += OnStatUpgradeActivated;
        StatPowerup.OnStatUpgradeDeactivated += OnStatUpgradeDeactivated;
    }

    private void OnDisable()
    {
        StatPowerup.OnStatUpgradeActivated -= OnStatUpgradeActivated;
        StatPowerup.OnStatUpgradeDeactivated -= OnStatUpgradeDeactivated;
    }

    private void OnStatUpgradeActivated(ScriptableStatUpgrade upgrade)
    {
        var powerupbarGo = Instantiate(powerUpBar, powerUpPanel.transform);
        // get text child in powerupbar
        var powerupbarText = powerupbarGo.GetComponentInChildren<TMP_Text>();
        var amount = upgrade.statType is StatType.Speed or StatType.PickupRange ||
                     upgrade.amount > 1
            ? upgrade.amount.ToString()
            : upgrade.amount.ToString("P0");
        powerupbarText.text = $"{upgrade.rewardName}\n+{amount}";
    }

    private void OnStatUpgradeDeactivated(ScriptableStatUpgrade _)
    {
        if (powerUpPanel && powerUpPanel.transform.childCount > 0)
        {
            var powerupbarGo = powerUpPanel.transform.GetChild(0)?.gameObject;
            if (powerupbarGo)
            {
                Destroy(powerupbarGo);
            }
        }
    }
}