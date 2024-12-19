using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatsButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI txtValue;
    [SerializeField] private TextMeshProUGUI txtCostNextLv;

    [field: SerializeField] public Button Button { get; private set; }

    public void Setup(StatData statData, PlayerStats playerStats)
    {
        UpdateVisuals(statData, playerStats);

        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() =>
        {
            float cost = playerStats.GetCostStatNextLV(statData.Stat);
            if (!CurrencyManager.Instance.HasEnoughCurrency(cost)) return;
            playerStats.UpgradeStat(statData.Stat);
            CurrencyManager.Instance.UseCurrency(cost);
        });
    }

    private void UpdateVisuals(StatData statData, PlayerStats playerStats)
    {
        txtName.text = statData.Stat.ToString();
        icon.sprite = statData.Icon;
        txtValue.text = playerStats.GetValueStat(statData.Stat).ToString();

        float cost = playerStats.GetCostStatNextLV(statData.Stat);
        txtCostNextLv.text = cost.ToString();

        Button.interactable = CurrencyManager.Instance.HasEnoughCurrency(cost);

    }


}
