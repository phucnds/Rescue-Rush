using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatsButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI txtValue;
    [SerializeField] private TextMeshProUGUI txtCostNextLv;

    [field: SerializeField] public Button Button { get; private set; }

    public void Setup(StatData statData)
    {
        UpdateVisuals(statData);

        Button.onClick.AddListener(() =>
        {
            float cost = Player.Instance.PlayerStats.GetCostStatNextLV(statData.Stat);
            Player.Instance.PlayerStats.UpgradeStat(statData.Stat);
            CurrencyManager.Instance.UseCurrency(cost);
            UpdateVisuals(statData);
        });
    }

    private void UpdateVisuals(StatData statData)
    {
        txtName.text = statData.Stat.ToString();
        icon.sprite = statData.Icon;
        txtValue.text = Player.Instance.PlayerStats.GetValueStat(statData.Stat).ToString();

        float cost = Player.Instance.PlayerStats.GetCostStatNextLV(statData.Stat);
        txtCostNextLv.text = cost.ToString();

        Button.interactable = CurrencyManager.Instance.HasEnoughCurrency(cost);
    }


}
